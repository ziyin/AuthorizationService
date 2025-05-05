using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Users;
using WebService.Authorization.Domain.Role.Interfaces;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Domain.UserRole.Interface;
using WebService.Authorization.Domain.UserRole.Model;
using WebService.Authorization.Domain.UserRole.Model.Parameter;
using WebService.Authorization.Shard.Extensions;

namespace WebService.Authorization.Application.AppService;

public class UserRoleAppService
    (
    IBindUserToRoleManager bindUserToRoleManager,
    IUserRoleRepository userRoleRepository
    ) : IUserRoleAppService
{
    private readonly IBindUserToRoleManager _bindUserToRoleManager = bindUserToRoleManager;
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;

    public async Task<int?> CreateAsync(CreateUserRoleParameterDto parameterDto)
    {
        var needCreateRoles = await _bindUserToRoleManager.GetCanBindRolesAsync(parameterDto.UserId, parameterDto.RoleIds);
        if (!needCreateRoles.IsAny())
        {
            return 0;
        }
        var muliEntities = UserRoleEntity.CreateManyRoles
            (
            userId: parameterDto.UserId,
            roleIds: needCreateRoles!,
            creator: parameterDto.Creator
            );
        await _userRoleRepository.CreateManyAsync(muliEntities);
        return muliEntities.Count();
    }

}