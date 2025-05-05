using WebService.Authorization.Application.Contracts.PrameterDtos.Users;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface IUserRoleAppService
{
    Task<int?> CreateAsync(CreateUserRoleParameterDto parameterDto);
}