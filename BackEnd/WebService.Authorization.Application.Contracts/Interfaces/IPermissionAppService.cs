using WebService.Authorization.Application.Contracts.PrameterDtos.Permission;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface IPermissionAppService
{
    Task CreateAsync(CreatePermissionParmeterDto parmeterDto);
    Task UpdateAsync(UpdatePermissionParamterDto parameterDto);
    Task SetEnalbeAsync(SetEnalbeParameterDto parameterDto);
}