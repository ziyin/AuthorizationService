using WebService.Authorization.Domain.UserRole.Model.Parameter;
using WebService.Authorization.Domain.UserRole.Model;

namespace WebService.Authorization.Domain.UserRole.Interface;

public interface IUserRoleRepository
{
    Task CreateAsync(UserRoleEntity userRoleEntity);
    Task<IEnumerable<UserRoleDataModel>?> GetListAsync(GetUserRoleListParameterModel parameterModel);
}