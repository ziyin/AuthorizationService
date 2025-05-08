using System.Security;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Permission;
using WebService.Authorization.Domain.Permission.Interface;
using WebService.Authorization.Domain.Permission.Model;

namespace WebService.Authorization.Application.AppService;

public class PermissionMaintainAppService
    (
    IPermissionRepository permissionRepository
    ) : IPermissionAppService
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async Task CreateAsync(CreatePermissionParmeterDto parameterDto)
    {
        var existCode = await GetPermissionAsync(permissionCode: parameterDto.Code);
        if (existCode is not null)
        {
            throw new ArgumentException($"{parameterDto.Code} is exist.");
        }
        var createEntity = PermissionEntity.Create
            (
            code: parameterDto.Code,
            name: parameterDto.Name,
            creator: parameterDto.Creator
            );
        await _permissionRepository.CreateAsync(createEntity);
    }

    public async Task UpdateAsync(UpdatePermissionParamterDto parameterDto)
    {
        var existCode = GetPermissionAsync(permissionCode: parameterDto.Code);
        var permission = await GetPermissionAsync(permissionId: parameterDto.PermissionId)
                         ?? throw new ArgumentException($"Permission not exist.");
        if ((await existCode) is not null)
        {
            throw new ArgumentException($"{parameterDto.Code} is exist.");
        }
        permission.Update
            (
            code: parameterDto.Code,
            name: parameterDto.Name,
            lastModifiedBy: parameterDto.LastModifiedBy
            );
        await _permissionRepository.UpdateAsync(permission);
    }

    public async Task SetEnalbeAsync(SetEnalbeParameterDto parameterDto)
    {
        var permission = await GetPermissionAsync(permissionId: parameterDto.PermissionId)
                 ?? throw new ArgumentException($"Permission not exist.");
        permission.SetEnableState
            (
            enable: parameterDto.Enable,
            modifiedBy: parameterDto.LastModifiedBy
            );
        await _permissionRepository.UpdateAsync(permission);
    }

    #region --Private

    private async Task<PermissionEntity?> GetPermissionAsync(Guid? permissionId = null, string? permissionCode = null)
    {
        return await _permissionRepository.GetAsync(new GetPermissionParameterModel
        {
            PermissionId = permissionId,
            PermissionCode = permissionCode
        });
    }

    #endregion
}