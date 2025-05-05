using MapsterMapper;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Users;
using WebService.Authorization.Application.Contracts.ResponseDtos.User;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Domain.User.Models.Parameters;
using WebService.Authorization.Shard.Extensions;

namespace WebService.Authorization.Application.AppService;

public class UserInformationAppService
    (
    IUserRepository userRepository,
    IMapper mapper
    ) : IUserInformationAppService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto?> GetAsync(Guid userId)
    {
        var user = await _userRepository.GetAsync(new GetUserParameterModel
        {
            UserId = userId
        });
        if (user is null)
        {
            return null;
        }
        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>?> GetListAsync(GetUserListParameterDto parameterDtos)
    {
        var userList = await _userRepository.GetListAsync(new GetUserListParameterModel
        {
            Name = parameterDtos.Name,
            Account = parameterDtos.Account,
            RegionBusinessUnit = parameterDtos.RegionBusinessUnit,
            Enable = parameterDtos.Enable
        });
        if (!userList.IsAny())
        {
            return null;
        }
        return _mapper.Map<IEnumerable<UserDto>>(userList!);
    }
}