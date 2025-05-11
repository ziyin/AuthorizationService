using MapsterMapper;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Roles;
using WebService.Authorization.Application.Contracts.ResponseDtos.Role;
using WebService.Authorization.Domain.Role.Interfaces;
using WebService.Authorization.Domain.Role.Models;

namespace WebService.Authorization.Application.AppService;

public class RoleInformationAppService
    (
    IRoleRepository roleRepository,
    IMapper mapper
    ) : IRoleInformationAppService
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<RoleDto?> GetAsync(GetRoleParameterDto parameterDto)
    {
        var result = await _roleRepository.GetAsync(new GetRoleParameterModel
        {
            RoleId = parameterDto.RoleId,
            RoleName = parameterDto.RoleName,
            Enable = parameterDto.Enable
        });
        return result is null ? null : _mapper.Map<RoleDto>(result);
    }

    public async Task<IEnumerable<RoleDto>?> GetListAsync(GetRoleListParameterDto parameterDto)
    {
        var result = await _roleRepository.GetListAsync(new GetRoleListParameterModel
        {
            RoleId = parameterDto.RoleId,
            RoleName = parameterDto.RoleName,
            Enable = parameterDto.Enable
        });

        return result is null ? null : _mapper.Map<IEnumerable<RoleDto>>(result);
    }
}