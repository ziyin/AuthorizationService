using WebService.Authorization.Domain.Role.Interfaces;
using WebService.Authorization.Domain.Role.Models;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Domain.User.Models.Parameters;
using WebService.Authorization.Domain.UserRole.Interface;
using WebService.Authorization.Domain.UserRole.Model.Parameter;

namespace WebService.Authorization.Domain.UserRole;

public class BindUserToRoleManager
    (
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUserRoleRepository userRoleRepository
    ) : IBindUserToRoleManager
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;

    public async Task<IEnumerable<Guid>?> GetCanBindRolesAsync(Guid userId, IEnumerable<Guid> roles)
    {
        await EnsureUserExistsAsync(userId);

        var currentRoleIds = await GetCurrentUserRoleIdsAsync(userId);
        var notBindingRoles = roles.Except(currentRoleIds);

        var existingRoles = await _roleRepository.GetListAsync(new GetRoleListParameterModel
        {
            RoleId = notBindingRoles
        });

        return existingRoles?.Select(x => x.Id) ?? [];
    }

    #region --Private

    private async Task EnsureUserExistsAsync(Guid userId)
    {
        _ = await _userRepository.GetAsync(new GetUserParameterModel
        {
            UserId = userId
        }) ?? throw new ArgumentException("User does not exist.");
    }

    private async Task<IEnumerable<Guid>> GetCurrentUserRoleIdsAsync(Guid userId)
    {
        var userRoles = await _userRoleRepository.GetListAsync(new GetUserRoleListParameterModel
        {
            UserId = userId
        });

        return userRoles?.Select(x => x.RoleId) ?? [];
    }
    #endregion
}