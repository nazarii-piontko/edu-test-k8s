using System.Collections;
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

        b.AppendLine("#### Request ####");
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
        b.AppendLine();

        b.AppendLine("#### Environment ####");
        foreach (DictionaryEntry pair in Environment.GetEnvironmentVariables())
        {
            b.Append(pair.Key);
            b.Append(" = ");
            b.Append(pair.Value);
            b.AppendLine();
        }
        
        return b.ToString();
    }
}
