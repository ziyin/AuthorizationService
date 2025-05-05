using CustomerAuthorization.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Users;
using WebService.Authorization.HttpApi.Request.User;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/authorization/[controller]")]
[ApiController]
public class UserController
    (
    IUserAppService userAppService,
    IGetCurrentUser getCurrentUser
    ) : ControllerBase
{
    private readonly IUserAppService _userAppService = userAppService;
    private readonly Guid _currentUserId = Guid.Parse(getCurrentUser.UserId);

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
        var createUserId = await _userAppService.CreateAsync(createParmeterDto);
        return Ok(createUserId);
    }

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
        await _userAppService.UpdateAsync(updateUserParameter);
        return NoContent();
    }

    [HttpPatch("{userId}")]
    public async Task<IActionResult> ResetPassword(Guid userId, [FromBody] ResetPasswordRequest request)
    {
        var resetParameterDto = new ResetPasswordParameterDto
        {
            UserId = userId,
            Password = request.Password,
            LastModifiedBy = _currentUserId
        };
        await _userAppService.ResetPasswordAsync(resetParameterDto);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAsync(Guid userId)
    {
        var setEnableParameterDto = new SetEnableParameterDto
        {
            UserId = userId,
            Enable = false,
            LastModifiedBy = _currentUserId
        };
        await _userAppService.SetEnableAsync(setEnableParameterDto);
        return NoContent();
    }
}