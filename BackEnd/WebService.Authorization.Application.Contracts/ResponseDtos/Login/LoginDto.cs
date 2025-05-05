namespace WebService.Authorization.Application.Contracts.ResponseDtos.Login;

public class LoginDto
{
    public string? AccessToken { get; set; }

    public string? ErrorMessage { get; set; }
}