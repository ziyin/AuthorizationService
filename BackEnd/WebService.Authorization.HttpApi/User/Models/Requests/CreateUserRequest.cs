namespace WebService.Authorization.HttpApi.User.Models.Requests;

public class CreateUserRequest
{
    public string Name { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string RegionBusinessUnit { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }
}