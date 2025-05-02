using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Users;
using WebService.Authorization.HttpApi.User.Models.Requests;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController
    (
    IUserAppService userAppService
    ) : ControllerBase
{
    private readonly IUserAppService _userAppService = userAppService;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request)
    {
        var createParmeterDto = new CreateUserParameterDto
        {
            Account = request.Account,
            Address = request.Address,
            Creator = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            Password = request.Password,
            Phone = request.Phone,
            RegionBusinessUnit = request.RegionBusinessUnit
        };
        var createUserId=await _userAppService.CreateAsync(createParmeterDto);
        return Ok(createUserId);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateAsync(Guid userId)
    {
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAsync()
    {
        return NoContent();
    }
}