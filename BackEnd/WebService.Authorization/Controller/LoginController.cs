using CustomerAuthorization.GenerateToken.Interfaces;
using CustomerAuthorization.GenerateToken.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Authorization.HttpApi.Host.Controller;

[Route("api/[controller]")]
[ApiController]
public class LoginController
    (
    IJwtTokenGenerator jwtTokenGenerator
    ): ControllerBase
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator= jwtTokenGenerator;

    [HttpGet]
    public async Task<IActionResult> LoginAsync()
    {
        var result = _jwtTokenGenerator.GenerateToken(new GenerateTokenParameter
        {
            Roles=new List<string> {"gfdg" },
            UserId="asf",
            UserName="123"
        });
        return Ok(result);
    }
}