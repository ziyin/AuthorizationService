using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface IRolePermissionAppService
{
    Task<int> CreateAsync(CreateRolePermissionParameterDto parameterDto);
}