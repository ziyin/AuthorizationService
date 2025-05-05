namespace WebService.Authorization.HttpApi.Request.User;

public class UpdateUserRequest
{
    public string Name { get; set; } = null!;
    public string RegionBusinessUnit { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
}