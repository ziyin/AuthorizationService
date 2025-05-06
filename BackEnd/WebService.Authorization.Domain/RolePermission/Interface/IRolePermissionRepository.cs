using WebService.Authorization.Domain.RolePermission.Model;
using WebService.Authorization.Shard.Options;

namespace WebService.Authorization.Domain.RolePermission.Interface;

public interface IRolePermissionRepository
{
    Task CreateAsync(RolePermissionEntity rolePermissionEntity);
    Task CreateManyAsync(IEnumerable<RolePermissionEntity> rolePermissionEntity);
    Task<IEnumerable<RolePermissionEntity>?> GetListAsync(GetRolePermissionListParameterModel parameterModel);
}