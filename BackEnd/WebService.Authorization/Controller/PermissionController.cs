using CustomerAuthorization.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Permission;
using WebService.Authorization.HttpApi.Request.Permission;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/authorization/[controller]")]
[ApiController]
public class PermissionController
    (
    IPermissionAppService permissionAppService,
    IGetCurrentUser getCurrentUser
    ) : ControllerBase
{
    private readonly IPermissionAppService _permissionAppService = permissionAppService;
    private readonly Guid _currentUserId = Guid.Parse(getCurrentUser.UserId);

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePermissionReuqest reuqest)
    {
        var parameterDto = new CreatePermissionParmeterDto
        {
            Code = reuqest.Code,
            Name = reuqest.Name,
            Creator = _currentUserId
        };
        await _permissionAppService.CreateAsync(parameterDto);
        return Created();
    }
}
