using CustomerAuthorization.Attributes;
using CustomerAuthorization.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;
using WebService.Authorization.HttpApi.Constant;
using WebService.Authorization.HttpApi.Repsonse.Role;
using WebService.Authorization.HttpApi.Request.Role;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/authorization/[controller]")]
[ApiController]
public class RoleController
    (
    IRoleMaintainAppService roleMaintainAppService,
    IRoleInformationAppService roleInformationAppService,
    IRolePermissionAppService rolePermissionAppService,
    IGetCurrentUser getCurrentUser,
    IMapper mapper
    ) : ControllerBase
{
    private readonly IRoleMaintainAppService _roleMaintainAppService = roleMaintainAppService;
    private readonly IRoleInformationAppService _roleInformationAppService = roleInformationAppService;
    private readonly IRolePermissionAppService _rolePermissionAppService = rolePermissionAppService;
    private readonly Guid _currentUserId = Guid.Parse(getCurrentUser.UserId);
    private readonly IMapper _mapper = mapper;

    [PermissionAuthorize(PermissionConstant.RoleEdit)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRoleRequest request)
    {
        var roleId = await _roleMaintainAppService.CreateAsync(new CreateRoleParameterDto
        {
            RoleName = request.RoleName,
            Creator = _currentUserId
        });
        return Ok(roleId);
    }

    [PermissionAuthorize(PermissionConstant.RoleEdit)]
    [HttpPut("{roleId}")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateRoleRequest request)
    {
        return NoContent();
    }

    [PermissionAuthorize(PermissionConstant.RoleEdit)]
    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteAsync(Guid roleId)
    {
        return NoContent();
    }

    [PermissionAuthorize(PermissionConstant.RoleEdit)]
    [HttpGet("{roleId}")]
    public async Task<IActionResult> GetAsync(Guid roleId)
    {
        var role = await _roleInformationAppService.GetAsync(new GetRoleParameterDto
        {
            RoleId = roleId
        });
        return role is null ? NotFound() : Ok(_mapper.Map<RoleResponse>(role));
    }

    [PermissionAuthorize(PermissionConstant.RoleRead)]
    [HttpGet("roles")]
    public async Task<IActionResult> GetListAsync([FromQuery]GetRoleListRequest request)
    {
        var roles = await _roleInformationAppService.GetListAsync(new GetRoleListParameterDto());
        var respnse = new RoleListResponse
        {
            List = _mapper.Map<IEnumerable<RoleResponse>>(roles ?? [])
        };
        return Ok(respnse);
    }

    [PermissionAuthorize(PermissionConstant.RoleEdit)]
    [HttpPost("set-permission/{roleId}")]
    public async Task<IActionResult> SetRolePermissionAsync(Guid roleId, [FromBody] SetRolePermissionRequest request)
    {
        var settingResult = await _rolePermissionAppService.CreateAsync(new CreateRolePermissionParameterDto
        {
            RoleId = roleId,
            Permissions = request.Permissions,
            Creator = _currentUserId
        });
        return settingResult > 0 ? Ok($"Binding {settingResult} permission(s).") : BadRequest("No permission are bound.");
    }

    [PermissionAuthorize(PermissionConstant.RoleRead)]
    [HttpGet("role-permission")]
    public async Task<IActionResult> GetRolePermissionsAsync([FromQuery] GetRolePermissionsRequest request)
    {
        var permissions = await _rolePermissionAppService.GetListAsync(new GetRolePermissionListParameterDto
        {
            RoleName = request.RoleName,
            RoleId = request.RoleId
        });
        var response = new RolePermissionListResponse
        {
            Data = permissions is null ? [] : _mapper.Map<IEnumerable<RolePermissionResponse>>(permissions)
        };
        return Ok(response);
    }
}
