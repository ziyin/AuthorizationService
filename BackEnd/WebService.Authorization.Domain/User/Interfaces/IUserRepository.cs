using WebService.Authorization.Domain.User.Models;
using WebService.Authorization.Domain.User.Models.Parameters;

namespace WebService.Authorization.Domain.User.Interfaces;

public interface IUserRepository
{
    Task<Guid> CreateAsync(CreateUserParameterModel parameterModel);

    Task<UserDataModel?> GetAsync(Guid userId);

    Task<IEnumerable<UserDataModel>?> GetListAsync(GetUserListParameterModel parameterModel);
}