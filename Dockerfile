FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONF=Release
WORKDIR /src
COPY ["TestK8s/TestK8s.csproj", "TestK8s/"]
RUN dotnet restore "TestK8s/TestK8s.csproj"
COPY . .
WORKDIR "/src/TestK8s"
RUN dotnet build "TestK8s.csproj" -c ${BUILD_CONF} -o /app/build

FROM build AS publish
ARG BUILD_CONF=Release
RUN dotnet publish "TestK8s.csproj" -c ${BUILD_CONF} -o /app/publish

FROM base AS final-dbg
RUN apt-get update && \
    apt-get install --no-install-recommends -y curl && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestK8s.dll"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestK8s.dll"]
