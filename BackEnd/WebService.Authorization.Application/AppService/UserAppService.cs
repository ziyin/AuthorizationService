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
        var hashedPassword = EncodePassword(createUserParameterDto.Password);
        var user = UserEntity.Create
        (
            name: createUserParameterDto.Name,
            account: createUserParameterDto.Account,
            hashedPassword: hashedPassword,
            regionBusinessUnit: createUserParameterDto.RegionBusinessUnit,
            email: createUserParameterDto.Email,
            phone: createUserParameterDto.Phone,
            address: createUserParameterDto.Address,
            creator: createUserParameterDto.Creator
        );
        var userId = await _userRepository.CreateAsync(user);
        return userId;
    }

    public async Task UpdateAsync(UpdateUserParameterDto parameterDto)
    {
        var getUserDetail = await GetUserAsync(parameterDto.UserId);
        getUserDetail.Update
            (
            name: parameterDto.Name,
            regionBusinessUnit: parameterDto.RegionBusinessUnit,
            email: parameterDto.Email,
            phone: parameterDto.Phone,
            address: parameterDto.Address,
            modifiedBy: parameterDto.LastModifiedBy
            );
        await _userRepository.UpdateAsync(getUserDetail);
    }

    public async Task ResetPasswordAsync(ResetPasswordParameterDto parameterDto)
    {
        var getUserDetail = await GetUserAsync(parameterDto.UserId);
        var hashedPassword = EncodePassword(parameterDto.Password);
        getUserDetail.ResetPassword(hashedPassword, parameterDto.LastModifiedBy);
        await _userRepository.UpdateAsync(getUserDetail);
    }

    public async Task SetEnableAsync(SetEnableParameterDto parameterDto)
    {
        var getUserDetail = await GetUserAsync(parameterDto.UserId);
        getUserDetail.SetEnableState(parameterDto.Enable, parameterDto.LastModifiedBy);
        await _userRepository.UpdateAsync(getUserDetail);
    }

    #region --private

    private async Task<UserEntity> GetUserAsync(Guid userId)
    {
        var getUserDetail = await _userRepository.GetAsync(new GetUserParameterModel
        {
            UserId = userId
        }) ?? throw new ArgumentException("User not exist.");
        return getUserDetail;
    }

    private string EncodePassword(string password)
    {
        var passwordHasher = new PasswordHasher<object>();
        string hashedPassword = passwordHasher.HashPassword(null!, password);
        return hashedPassword;
    }
    #endregion
}