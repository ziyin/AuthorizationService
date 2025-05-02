using Microsoft.AspNetCore.Identity;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Users;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Domain.User.Models.Parameters;

namespace WebService.Authorization.Application.AppService;

public class UserAppService
    (
    IUserRepository userRepository
    ) : IUserAppService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Guid> CreateAsync(CreateUserParameterDto createUserParameterDto)
    {
        var checkAccount = await _userRepository.GetListAsync(new GetUserListParameterModel
        {
            Account = createUserParameterDto.Account
        });
        if (checkAccount!.Any())
        {
             throw new ArgumentException("Account exist.");
        }

        var passwordHasher = new PasswordHasher<object>();
        string hashedPassword = passwordHasher.HashPassword(null!, createUserParameterDto.Password);
        var parameterModel = new CreateUserParameterModel
        {
            Account = createUserParameterDto.Account,
            Address = createUserParameterDto.Address,
            Creator = createUserParameterDto.Creator,
            Email = createUserParameterDto.Email,
            Name = createUserParameterDto.Name,
            Password = hashedPassword,
            Phone = createUserParameterDto.Phone,
            RegionBusinessUnit = createUserParameterDto.RegionBusinessUnit
        };
        var userId = await _userRepository.CreateAsync(parameterModel);
        return userId;
    }
}