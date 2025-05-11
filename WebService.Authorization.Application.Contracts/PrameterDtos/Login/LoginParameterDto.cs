namespace WebService.Authorization.Application.Contracts.PrameterDtos.Login;

public class LoginParameterDto
{
    public string Account { get; set; } = null!;
    public string Password { get; set; } = null!;
}