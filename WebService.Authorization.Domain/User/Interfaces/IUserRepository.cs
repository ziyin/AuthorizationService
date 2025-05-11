using WebService.Authorization.Domain.User.Models;
using WebService.Authorization.Domain.User.Models.Parameters;

namespace WebService.Authorization.Domain.User.Interfaces;

public interface IUserRepository
{
    Task<Guid> CreateAsync(UserEntity parameterModel);
    Task UpdateAsync(UserEntity userEntity);
    Task<UserEntity?> GetAsync(GetUserParameterModel parameterModel);
    Task<IEnumerable<UserEntity>?> GetListAsync(GetUserListParameterModel parameterModel);
}