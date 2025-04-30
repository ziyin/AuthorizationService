using Microsoft.AspNetCore.Mvc;

namespace WebService.Authorization.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Test()
    {
        return Ok();
    }
}
