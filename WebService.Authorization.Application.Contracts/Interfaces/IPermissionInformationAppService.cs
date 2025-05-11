using WebService.Authorization.Application.Contracts.PrameterDtos.Permission;
using WebService.Authorization.Application.Contracts.ResponseDtos.Permission;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface IPermissionInformationAppService
{
    Task<PermissionDto?> GetAsync(GetPermissionParameterDto parameterDto);
    Task<IEnumerable<PermissionDto>?> GetListAsync(GetPermissionListParameterDto parameterDto);
}