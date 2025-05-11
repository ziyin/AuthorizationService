using WebService.Authorization.Application.Contracts.PrameterDtos.Login;
using WebService.Authorization.Application.Contracts.ResponseDtos.Login;

namespace WebService.Authorization.Application.Contracts.Interfaces;

public interface ILoginAppService
{
    Task<LoginDto> HandleAsync(LoginParameterDto parameterDto);
}