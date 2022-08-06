using Microsoft.AspNetCore.Mvc;

namespace TestK8s.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return RedirectToAction("Get", "Hello");
    }
}
