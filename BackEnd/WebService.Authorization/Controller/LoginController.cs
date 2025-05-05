using Microsoft.AspNetCore.Mvc;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.HttpApi.Request.Login;


namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/authorization/[controller]")]
[ApiController]
public class LoginController
    (
    ILoginAppService loginAppService
    ) : ControllerBase
{
    private readonly ILoginAppService _loginAppService = loginAppService;

    [HttpPost]
    public async Task<IActionResult> HandleAsync(LoginRequest loginRequest)
    {
        var result = await _loginAppService.HandleAsync(new Application.Contracts.PrameterDtos.Login.LoginParameterDto
        {
            Account = loginRequest.Account,
            Password = loginRequest.Password,
        });
        return string.IsNullOrWhiteSpace(result.AccessToken) ? Forbid(result.ErrorMessage!) : Ok(result.AccessToken!);
    }
}