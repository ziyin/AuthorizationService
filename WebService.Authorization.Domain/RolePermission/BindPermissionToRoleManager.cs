using System.Collections.Generic;
using WebService.Authorization.Domain.Permission.Interface;
using WebService.Authorization.Domain.Permission.Model;
using WebService.Authorization.Domain.Role.Interfaces;
using WebService.Authorization.Domain.Role.Models;
using WebService.Authorization.Domain.RolePermission.Interface;
using WebService.Authorization.Domain.RolePermission.Model;
using WebService.Authorization.Shard.Extensions;

namespace WebService.Authorization.Domain.RolePermission;

public class BindPermissionToRoleManager
    (
    IPermissionRepository permissionRepository,
    IRoleRepository roleRepository,
    IRolePermissionRepository rolePermissionRepository
    ) : IBindPermissionToRoleManager
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;

    public async Task<IEnumerable<Guid>?> GetCanBindPermissionAsync(Guid roleId, IEnumerable<Guid> permissionId)
    {
        await EnsureRoleExistAsync(roleId);

        var currentPermissionIds = await GetCurrentRolePermissionIdsAsync(roleId);
        var notBindingPermissions = permissionId.Except(currentPermissionIds);

        var existingPermssions = await _permissionRepository.GetListAsync(new GetPermissionListParameterModel
        {
            PermissionId = notBindingPermissions
        });

        return existingPermssions?.Select(x => x.Id) ?? [];
    }

    public async Task<IEnumerable<RolePermissionEntity>?> GetRolePermissionAsync(GetRolePermissionParameterModel parameterModel)
    {
        if (parameterModel.RoleNames.IsAny())
        {
            var roleIds = await GetRoleIdAsync(parameterModel.RoleNames!);
            parameterModel.RoleIds.AddRange(roleIds ?? []);
        }
        return await _rolePermissionRepository.GetListAsync(new GetRolePermissionListParameterModel
        {
            RoleId = parameterModel.RoleIds
        });
    }

    #region --Private

    private async Task EnsureRoleExistAsync(Guid roleId)
    {
        _ = await _roleRepository.GetAsync(new GetRoleParameterModel
        {
            RoleId = roleId
        }) ?? throw new ArgumentException("Role does not exist.");
    }

    private async Task<IEnumerable<Guid>> GetCurrentRolePermissionIdsAsync(Guid roleId)
    {
        var rolePermissions = await _rolePermissionRepository.GetListAsync(new GetRolePermissionListParameterModel
        {
            RoleId = [roleId]
        });

        return rolePermissions?.Select(x => x.PermissionId) ?? [];
    }

    private async Task<IEnumerable<Guid>?> GetRoleIdAsync(IEnumerable<string> roleName)
    {
        var result = await _roleRepository.GetListAsync(new GetRoleListParameterModel
        {
            RoleName = roleName
        });
        return result?.Select(item => item.Id);
    }

    #endregion
}