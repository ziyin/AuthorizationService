using WebService.Authorization.Application.Contracts.PrameterDtos.Users;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface IUserAppService
{
    Task<Guid> CreateAsync(CreateUserParameterDto createUserParameterDto);
}