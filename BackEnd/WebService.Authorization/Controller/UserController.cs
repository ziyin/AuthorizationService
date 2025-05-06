using CustomerAuthorization.Attributes;
using CustomerAuthorization.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Users;
using WebService.Authorization.Application.Contracts.ResponseDtos.User;
using WebService.Authorization.HttpApi.Constant;
using WebService.Authorization.HttpApi.Request.User;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/authorization/[controller]")]
[ApiController]
public class UserController
    (
    IUserInformationAppService userInformationAppService,
    IUserMaintainAppService userMaintainAppService,
    IUserRoleAppService userRoleAppService,
    IGetCurrentUser getCurrentUser
    ) : ControllerBase
{
    private readonly IUserInformationAppService _userInformationAppService = userInformationAppService;
    private readonly IUserMaintainAppService _userMaintainAppService = userMaintainAppService;
    private readonly IUserRoleAppService _userRoleAppService = userRoleAppService;
    private readonly Guid _currentUserId = Guid.Parse(getCurrentUser.UserId);

    [PermissionAuthorize(PermissionConstant.UserRead)]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAsync(Guid userId)
    {
        var userData = await _userInformationAppService.GetAsync(userId);
        return userData is null ? NotFound() : Ok(userData);
    }

    [PermissionAuthorize(PermissionConstant.UserRead)]
    [HttpGet("list")]
    public async Task<IActionResult> GetListAsync([FromQuery] GetUserListRequest request)
    {
        var userDatas = await _userInformationAppService.GetListAsync(new GetUserListParameterDto
        {
            Name = request.Name,
            Account = request.Account,
            RegionBusinessUnit = request.RegionBusinessUnit,
            Enable = request.Enable
        });
        return userDatas is null ? Ok(Enumerable.Empty<UserDto>()) : Ok(userDatas);
    }

    [PermissionAuthorize(PermissionConstant.UserAdmin)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request)
    {
        var createParmeterDto = new CreateUserParameterDto
        {
            Account = request.Account,
            Address = request.Address,
            Creator = _currentUserId,
            Email = request.Email,
            Name = request.Name,
            Password = request.Password,
            Phone = request.Phone,
            RegionBusinessUnit = request.RegionBusinessUnit
        };
        var createUserId = await _userMaintainAppService.CreateAsync(createParmeterDto);
        return Ok(createUserId);
    }

    [PermissionAuthorize(PermissionConstant.UserEdit)]
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateAsync(Guid userId, [FromBody] UpdateUserRequest request)
    {
        var updateUserParameter = new UpdateUserParameterDto
        {
            UserId = userId,
            Address = request.Address,
            Email = request.Email,
            Name = request.Name,
            Phone = request.Phone,
            RegionBusinessUnit = request.RegionBusinessUnit,
            LastModifiedBy = _currentUserId
        };
        await _userMaintainAppService.UpdateAsync(updateUserParameter);
        return NoContent();
    }

    [PermissionAuthorize(PermissionConstant.UserEdit)]
    [HttpPatch("{userId}")]
    public async Task<IActionResult> ResetPassword(Guid userId, [FromBody] ResetPasswordRequest request)
    {
        var resetParameterDto = new ResetPasswordParameterDto
        {
            UserId = userId,
            Password = request.Password,
            LastModifiedBy = _currentUserId
        };
        await _userMaintainAppService.ResetPasswordAsync(resetParameterDto);
        return NoContent();
    }

    [PermissionAuthorize(PermissionConstant.UserAdmin)]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAsync(Guid userId)
    {
        var setEnableParameterDto = new SetEnableParameterDto
        {
            UserId = userId,
            Enable = false,
            LastModifiedBy = _currentUserId
        };
        await _userMaintainAppService.SetEnableAsync(setEnableParameterDto);
        return NoContent();
    }

    [PermissionAuthorize(PermissionConstant.UserAdmin)]
    [HttpPost("set-roles/{userId}")]
    public async Task<IActionResult> SetRoleAsync(Guid userId, [FromBody] SetUserRoleRequest request)
    {
        var settingCount = await _userRoleAppService.CreateAsync(new CreateUserRoleParameterDto
        {
            UserId = userId,
            RoleIds = request.RoleIds,
            Creator = _currentUserId
        });
        return settingCount>0?Ok($"Binding {settingCount} role(s)."):BadRequest("No roles are bound.");
    }
}