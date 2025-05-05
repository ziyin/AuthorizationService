using WebService.Authorization.Domain.User.Models;
using WebService.Authorization.Domain.User.Models.Parameters;

namespace WebService.Authorization.Domain.User.Interfaces;

public interface IUserRepository
{
    Task<Guid> CreateAsync(UserEntity parameterModel);

    Task<UserDataModel?> GetAsync(GetUserParameterModel parameterModel);

    Task<IEnumerable<UserDataModel>?> GetListAsync(GetUserListParameterModel parameterModel);
}