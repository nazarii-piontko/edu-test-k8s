using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace TestK8s.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        var r = Request;
        var b = new StringBuilder();

        b.AppendLine("Hello World!");
        b.AppendLine();

        b.AppendFormat("{0} {1} {2}", r.Scheme.ToUpperInvariant(), r.Method, r.Path.ToUriComponent());
        b.AppendLine();

        foreach (var header in r.Headers)
        {
            foreach (var value in header.Value)
            {
                b.Append(header.Key);
                b.Append(" = ");
                b.AppendLine(value);
            }
        }
        
        return b.ToString();
    }
}
