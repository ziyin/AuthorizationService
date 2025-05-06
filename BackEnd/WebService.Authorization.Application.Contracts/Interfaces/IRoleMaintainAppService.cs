using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface IRoleMaintainAppService
{
    Task<Guid> CreateAsync(CreateRoleParameterDto parameterDto);
}