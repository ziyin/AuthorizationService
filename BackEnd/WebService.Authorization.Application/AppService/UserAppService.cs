using Microsoft.AspNetCore.Identity;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Application.Contracts.PrameterDtos.Users;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Domain.User.Models;
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
        var user = UserEntity.Create
        (
            name: createUserParameterDto.Name,
            account: createUserParameterDto.Account,
            hashedPassword: hashedPassword,
            regionBusinessUnit: createUserParameterDto.RegionBusinessUnit,
            email:createUserParameterDto.Email,
            phone:createUserParameterDto.Phone,
            address: createUserParameterDto.Address,
            creator:createUserParameterDto.Creator
        );
        var userId = await _userRepository.CreateAsync(user);
        return userId;
    }
}