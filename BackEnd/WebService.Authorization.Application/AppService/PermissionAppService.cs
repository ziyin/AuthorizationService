using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Permission;
using WebService.Authorization.Domain.Permission.Interface;
using WebService.Authorization.Domain.Permission.Model;

namespace WebService.Authorization.Application.AppService;

public class PermissionAppService
    (
    IPermissionRepository permissionRepository
    ) : IPermissionAppService
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async Task CreateAsync(CreatePermissionParmeterDto parmeterDto)
    {
        var createEntity = PermissionEntity.Create
            (
            code: parmeterDto.Code,
            name: parmeterDto.Name,
            creator: parmeterDto.Creator
            );
        await _permissionRepository.CreateAsync(createEntity);
    }
}