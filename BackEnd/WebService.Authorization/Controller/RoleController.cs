using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;
using WebService.Authorization.HttpApi.Request.Role;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/authorization/[controller]")]
[ApiController]
public class RoleController
    (
    IRoleAppService roleAppService
    ) : ControllerBase
{
    private readonly IRoleAppService _roleAppService = roleAppService;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRoleRequest request)
    {
        var parameterDto = new CreateRoleParameterDto
        {
            RoleName = request.RoleName
        };
        var roleId = await _roleAppService.CreateAsync(parameterDto);
        return Ok(roleId);
    }

    [HttpPut("{roleId}")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateRoleRequest request)
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
