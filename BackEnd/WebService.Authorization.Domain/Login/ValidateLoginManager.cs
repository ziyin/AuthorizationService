using Microsoft.AspNetCore.Identity;
using WebService.Authorization.Domain.Login.Interfaces;
using WebService.Authorization.Domain.User.Models;

namespace WebService.Authorization.Domain.Login;

public class ValidateLoginManager : IValidateLoginManager
{
    public string Handle(UserEntity userDomainEntity, string inputPassowrd)
    {
        if (!userDomainEntity.Enable)
        {
            return "Account is not enable";
        }
        var passwordHasher = new PasswordHasher<object>();
        var passwordVerifyResult = passwordHasher.VerifyHashedPassword(null!, userDomainEntity.Password, inputPassowrd);
        return passwordVerifyResult == PasswordVerificationResult.Success ? string.Empty : "Password error.";
    }
}