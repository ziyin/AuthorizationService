using WebService.Authorization.Application.Contracts.PrameterDtos.Users;
using WebService.Authorization.Application.Contracts.ResponseDtos.User;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface IUserInformationAppService
{
    Task<UserDto?> GetAsync(Guid userId);
    Task<IEnumerable<UserDto>?> GetListAsync(GetUserListParameterDto parameterDtos);
}