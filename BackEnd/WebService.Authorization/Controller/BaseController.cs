using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;
using WebService.Authorization.Application.Contracts.PrameterDtos.Users;
using WebService.Authorization.HttpApi.Constant;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/authorization/[controller]")]
[ApiController]
public class BaseController
    (
    IUserMaintainAppService userMaintainAppService,
    IUserInformationAppService userInformationAppService,
    IRoleMaintainAppService roleMaintainAppService,
    IRolePermissionAppService rolePermissionAppService,
    IRoleInformationAppService roleInformationAppService,
    IUserRoleAppService userRoleAppService
    ) : ControllerBase
{
    private readonly IUserMaintainAppService _userMaintainAppService = userMaintainAppService;
    private readonly IUserInformationAppService _userInformationAppService = userInformationAppService;
    private readonly IRoleMaintainAppService _roleMaintainAppService = roleMaintainAppService;
    private readonly IRolePermissionAppService _rolePermissionAppService = rolePermissionAppService;
    private readonly IRoleInformationAppService _roleInformationAppService = roleInformationAppService;
    private readonly IUserRoleAppService _userRoleAppService = userRoleAppService;
    private const string _account = "service-admin";
    private const string _password = "strongPass@word";
    private Dictionary<string, IEnumerable<string>> _rolePermissionMapping = new()
    {
            { "RoleRead",[PermissionConstant.RoleRead]},
            { "PermissionRead",[PermissionConstant.PermissionRead]},
            { "RoleAdmin",[PermissionConstant.RoleRead,PermissionConstant.RoleEdit]},
            { "UserMaintain",[PermissionConstant.UserEdit]},
            { "UserAdmin",[PermissionConstant.UserEdit,PermissionConstant.UserRead,PermissionConstant.UserAdmin]},
            { "PermissionAdmin",[PermissionConstant.PermissionRead,PermissionConstant.PermissionRead]},
            { "UserRead",[PermissionConstant.UserRead]},
    };

    [HttpPost("init-sync")]
    public async Task<IActionResult> InitSyncAsync()
    {
        var userId = await GetUserId();
        await CreateRoleAndPermission(userId);
        await SyncRoles(userId);
        var defaultInformaion = new
        {
            Account = _account,
            Password = _password,
            Message = "Please reset password quickly"
        };
        return Ok(defaultInformaion);
    }



    #region --Private

    private async Task<Guid> GetUserId()
    {
        var userExist = await _userInformationAppService.GetAsync(new GetUserParameterDto
        {
            Account = _account
        });
        if (userExist?.Id is null)
        {
            return await _userMaintainAppService.CreateAsync(new CreateUserParameterDto
            {
                Account = _account,
                Password = _password,
                Name = "Service-Admin",
                RegionBusinessUnit = "Admin",
                Creator = Guid.Empty
            });
        }
        return userExist.Id;
    }

    private async Task CreateRoleAndPermission(Guid userId)
    {
        foreach (var mapping in _rolePermissionMapping)
        {
            var roleExist = await _roleInformationAppService.GetAsync(new GetRoleParameterDto
            {
                RoleName = mapping.Key,
            });
            var roleId = roleExist?.Id;
            roleId ??= await _roleMaintainAppService.CreateAsync(new CreateRoleParameterDto
            {
                RoleName = mapping.Key,
                Creator = userId
            });
            //ToDO : Create Permission and Binding
        }
    }

    private async Task SyncRoles(Guid userId)
    {
        var allRoles = await _roleInformationAppService.GetListAsync(new GetRoleListParameterDto());
        await _userRoleAppService.CreateAsync(new CreateUserRoleParameterDto
        {
            UserId = userId,
            RoleIds = allRoles?.Select(item => item.Id) ?? [],
            Creator = userId
        });
    }

    #endregion
}