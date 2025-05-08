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
        var checkRole = await GetRoleData(roleName: parameterDto.RoleName);
        if (checkRole is not null)
        {
            throw new ArgumentException($"{parameterDto.RoleName} exist.");
        }

        var role = RoleEntity.Create
        (
            name: parameterDto.RoleName,
            creator: parameterDto.Creator
        );
        var roleId = await _roleRepository.CreateAsync(role);
        return roleId;
    }

    public async Task UpdateAsync(UpdateRoleParameterDto parameterDto)
    {
        var checkRole = GetRoleData(roleName: parameterDto.RoleName);
        var currentRoleData = await GetRoleData(roleId: parameterDto.RoleId) ?? throw new ArgumentException("Role not exist.");
        if ((await checkRole) is not null)
        {
            throw new ArgumentException($"{parameterDto.RoleName} exist.");
        }
        currentRoleData.Update(
            name: parameterDto.RoleName,
            modifiedBy: parameterDto.LastModifiedBy
            );
        await _roleRepository.UpdateAsync(currentRoleData);
    }

    public async Task SetRoleEnableAsync(SetRoleEnableParameterDto parameterDto)
    {
        var currentRoleData = await GetRoleData(roleId: parameterDto.RoleId) ?? throw new ArgumentException("Role not exist.");
        currentRoleData.SetEnableState
            (
            enable: parameterDto.Enable,
            modifiedBy: parameterDto.LastModifiedBy
            );
        await _roleRepository.UpdateAsync(currentRoleData);
    }

    #region --Private

    private async Task<RoleEntity?> GetRoleData(string? roleName = null, Guid? roleId = null)
    {
        var checkRole = await _roleRepository.GetAsync(new GetRoleParameterModel
        {
            RoleId = roleId,
            RoleName = roleName
        });
        return checkRole;
    }

    #endregion
}