using WebService.Authorization.Application.Contracts.PrameterDtos.Users;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface IUserMaintainAppService
{
    Task<Guid> CreateAsync(CreateUserParameterDto createUserParameterDto);
    Task UpdateAsync(UpdateUserParameterDto parameterDto);
    Task ResetPasswordAsync(ResetPasswordParameterDto parameterDto);
    Task SetEnableAsync(SetEnableParameterDto parameterDto);
}