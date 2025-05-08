using WebService.Authorization.Domain.Permission.Model;

namespace WebService.Authorization.Domain.Permission.Interface;

public interface IPermissionRepository
{
    Task CreateAsync(PermissionEntity permissionEntity);
    Task UpdateAsync(PermissionEntity permissionEntity);
    Task<PermissionEntity?> GetAsync(GetPermissionParameterModel parameterModel);
    Task<IEnumerable<PermissionEntity>?> GetListAsync(GetPermissionListParameterModel parameterModel);
}