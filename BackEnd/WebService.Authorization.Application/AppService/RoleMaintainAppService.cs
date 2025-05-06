using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;
using WebService.Authorization.Domain.Role.Interfaces;
using WebService.Authorization.Domain.Role.Models;

namespace WebService.Authorization.Application.AppService;
public class RoleMaintainAppService
    (
    IRoleRepository roleRepository
    ) : IRoleMaintainAppService
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<Guid> CreateAsync(CreateRoleParameterDto parameterDto)
    {
        var checkRole = await _roleRepository.GetAsync(new GetRoleParameterModel
        {
            RoleName = parameterDto.RoleName
        });
        if (checkRole is not null)
        {
            throw new ArgumentException("Roles exist.");
        }

        var role = RoleEntity.Create
        (
            name: parameterDto.RoleName,
            creator: parameterDto.Creator
        );
        var roleId = await _roleRepository.CreateAsync(role);
        return roleId;
    }
}