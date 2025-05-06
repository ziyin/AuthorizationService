using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;
using WebService.Authorization.Application.Contracts.ResponseDtos.Role;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface IRoleInformationAppService
{
    Task<RoleDto?> GetAsync(GetRoleParameterDto parameterDto);
    Task<IEnumerable<RoleDto>?> GetListAsync(GetRoleListParameterDto parameterDto);
}