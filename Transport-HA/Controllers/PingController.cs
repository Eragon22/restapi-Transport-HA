using Microsoft.AspNetCore.Mvc;

namespace Bme.Swlab1.Rest.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class PingController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Ping()
    {
        return Ok("pong");
    }
}
