namespace WebService.Authorization.HttpApi.Request.Login;

public class LoginRequest
{
    public string Account { get; set; } = null!;
    public string Password { get; set; } = null!;
}