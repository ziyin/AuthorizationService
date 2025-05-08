using CustomerAuthorization.Attributes;
using CustomerAuthorization.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Permission;
using WebService.Authorization.HttpApi.Constant;
using WebService.Authorization.HttpApi.Repsonse.Permission;
using WebService.Authorization.HttpApi.Request.Permission;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/authorization/[controller]")]
[ApiController]
public class PermissionController
    (
    IPermissionAppService permissionAppService,
    IPermissionInformationAppService permissionInformationAppService,
    IGetCurrentUser getCurrentUser,
    IMapper mapper
    ) : ControllerBase
{
    private readonly IPermissionAppService _permissionAppService = permissionAppService;
    private readonly IPermissionInformationAppService _permissionInformationAppService = permissionInformationAppService;
    private readonly Guid _currentUserId = Guid.Parse(getCurrentUser.UserId);
    private readonly IMapper _mapper = mapper;

    [PermissionAuthorize(PermissionConstant.PermissionEdit)]
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

    [PermissionAuthorize(PermissionConstant.PermissionEdit)]
    [HttpPut("{permissionId}")]
    public async Task<IActionResult> UpdateAsync(Guid permissionId, [FromBody] UpdatePermissionRequest request)
    {
        await _permissionAppService.UpdateAsync(new UpdatePermissionParamterDto
        {
            PermissionId = permissionId,
            Code = request.Code,
            Name = request.Name,
            LastModifiedBy = _currentUserId
        });
        return NoContent();
    }

    [PermissionAuthorize(PermissionConstant.PermissionEdit)]
    [HttpDelete("{permissionId}")]
    public async Task<IActionResult> DeleteAsync(Guid permissionId)
    {
        await _permissionAppService.SetEnalbeAsync(new SetEnalbeParameterDto
        {
            PermissionId = permissionId,
            Enable = false,
            LastModifiedBy = _currentUserId
        });
        return NoContent();
    }

    [PermissionAuthorize(PermissionConstant.PermissionRead)]
    [HttpGet("permissions")]
    public async Task<IActionResult> GetListAsync()
    {
        var permissions = await _permissionInformationAppService.GetListAsync(new GetPermissionListParameterDto());
        var response = new PermissionListResponse
        {
            List = _mapper.Map<IEnumerable<PermissionResponse>>(permissions ?? [])
        };
        return Ok(response);
    }
}
