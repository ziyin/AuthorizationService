using CustomerAuthorization.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;
using WebService.Authorization.HttpApi.Request.Role;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/authorization/[controller]")]
[ApiController]
public class RoleController
    (
    IRoleAppService roleAppService,
    IRolePermissionAppService rolePermissionAppService,
    IGetCurrentUser getCurrentUser
    ) : ControllerBase
{
    private readonly IRoleAppService _roleAppService = roleAppService;
    private readonly IRolePermissionAppService _rolePermissionAppService = rolePermissionAppService;
    private readonly Guid _currentUserId = Guid.Parse(getCurrentUser.UserId);

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRoleRequest request)
    {
        var parameterDto = new CreateRoleParameterDto
        {
            RoleName = request.RoleName,
            Creator = _currentUserId
        };
        var roleId = await _roleAppService.CreateAsync(parameterDto);
        return Ok(roleId);
    }

    [HttpPut("{roleId}")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateRoleRequest request)
    {
        return NoContent();
    }

    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteAsync(Guid roleId)
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

    [HttpPost("set-permission/{roleId}")]
    public async Task<IActionResult> SetRolePermissionAsync(Guid roleId, [FromBody] SetRolePermissionRequest request)
    {
        var settingResult = await _rolePermissionAppService.CreateAsync(new CreateRolePermissionParameterDto
        {
            RoleId= roleId,
            Permissions = request.Permissions,
            Creator= _currentUserId
        });
        return settingResult > 0 ? Ok($"Binding {settingResult} permission(s).") : BadRequest("No permission are bound.");
    }
}
