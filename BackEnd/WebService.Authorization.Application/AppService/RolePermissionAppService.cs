using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;
using WebService.Authorization.Domain.RolePermission.Interface;
using WebService.Authorization.Domain.RolePermission.Model;
using WebService.Authorization.Shard.Extensions;

namespace WebService.Authorization.Application.AppService;

public class RolePermissionAppService
    (
    IBindPermissionToRoleManager bindPermissionToRoleManager,
    IRolePermissionRepository rolePermissionRepository
    ) : IRolePermissionAppService
{
    private readonly IBindPermissionToRoleManager _bindPermissionToRoleManager = bindPermissionToRoleManager;
    private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;

    public async Task<int> CreateAsync(CreateRolePermissionParameterDto parameterDto)
    {
        var needCreatePermissions = await _bindPermissionToRoleManager.GetCanBindPermissionAsync(parameterDto.RoleId, parameterDto.Permissions);
        if (!needCreatePermissions.IsAny())
        {
            return 0;
        }
        var muliEntities = RolePermissionEntity.CreateManyPermissions
            (
            roleId: parameterDto.RoleId,
            permissionIds: needCreatePermissions!,
            creator: parameterDto.Creator
            );
        await _rolePermissionRepository.CreateManyAsync(muliEntities);
        return muliEntities.Count();
    }
}
