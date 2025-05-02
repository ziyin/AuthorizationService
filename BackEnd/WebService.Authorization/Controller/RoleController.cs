using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.HttpApi.Role.Models;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/authorization/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] AddRoleRequest request)
    {
        return Created();
    }

    [HttpPut("{roleId}")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateRequest request)
    {
        return NoContent();
    }

    [HttpGet("{roleId}")]
    public async Task<IActionResult> GetAsync(Guid roleId)
    {
        return Ok();
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetListAsync()
    {
        return Ok();
    }
}
