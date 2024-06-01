using Microsoft.AspNetCore.Mvc;

namespace PolisProReminder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController
{
    [HttpPatch("user")]
    //public async Task<IActionResult> UpdateUsersDetails()
    public void UpdateUsersDetails()

    {

    }
}
