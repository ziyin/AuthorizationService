using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;
using WebService.Authorization.Application.Contracts.ResponseDtos.Role;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface IRolePermissionAppService
{
    Task<int> CreateAsync(CreateRolePermissionParameterDto parameterDto);
    Task<IEnumerable<RolePermissionDto>?> GetListAsync(GetRolePermissionListParameterDto parameterDto);
}