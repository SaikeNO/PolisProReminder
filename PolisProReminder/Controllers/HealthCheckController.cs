using Microsoft.AspNetCore.Mvc;

namespace PolisProReminder.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public ActionResult HealthCheck()
    {
        return Ok(DateTime.UtcNow);
    }
}
