FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestK8s/TestK8s.csproj", "TestK8s/"]
RUN dotnet restore "TestK8s/TestK8s.csproj"
COPY . .
WORKDIR "/src/TestK8s"
RUN dotnet build "TestK8s.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestK8s.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestK8s.dll"]
