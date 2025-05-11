using MapsterMapper;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Permission;
using WebService.Authorization.Application.Contracts.ResponseDtos.Permission;
using WebService.Authorization.Domain.Permission.Interface;
using WebService.Authorization.Domain.Permission.Model;

namespace WebService.Authorization.Application.AppService;

public class PermissionInformationAppService
    (
    IPermissionRepository permissionRepository,
    IMapper mapper
    ) : IPermissionInformationAppService
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PermissionDto?> GetAsync(GetPermissionParameterDto parameterDto)
    {
        var entity = await _permissionRepository.GetAsync(new GetPermissionParameterModel
        {
            PermissionId = parameterDto.Id,
            PermissionCode = parameterDto.Code
        });
        return entity is null ? null : _mapper.Map<PermissionDto>(entity);
    }

    public async Task<IEnumerable<PermissionDto>?> GetListAsync(GetPermissionListParameterDto parameterDto)
    {
        var entity = await _permissionRepository.GetListAsync(new GetPermissionListParameterModel
        {
        });
        return entity is null ? null : _mapper.Map<IEnumerable<PermissionDto>>(entity);
    }
}