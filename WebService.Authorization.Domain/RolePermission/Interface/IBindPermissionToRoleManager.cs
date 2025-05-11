using WebService.Authorization.Domain.RolePermission.Model;

namespace WebService.Authorization.Domain.RolePermission.Interface;

public interface IBindPermissionToRoleManager
{
    Task<IEnumerable<Guid>?> GetCanBindPermissionAsync(Guid roleId, IEnumerable<Guid> permissionId);
    Task<IEnumerable<RolePermissionEntity>?> GetRolePermissionAsync(GetRolePermissionParameterModel parameterModel);
}