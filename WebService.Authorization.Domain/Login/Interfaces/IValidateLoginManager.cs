using WebService.Authorization.Domain.User.Models;

namespace WebService.Authorization.Domain.Login.Interfaces;

public interface IValidateLoginManager
{
    string Handle(UserEntity userDomainEntity, string inputPassowrd);
}