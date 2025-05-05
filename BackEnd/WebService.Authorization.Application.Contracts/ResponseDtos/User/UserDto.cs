namespace WebService.Authorization.Application.Contracts.ResponseDtos.User;

public class UserDto
{
    public Guid Id { get;  set; }
    public string Name { get;  set; } = null!;
    public string? RegionBusinessUnit { get;  set; }
    public string? Email { get;  set; }
    public string? Phone { get;  set; }
    public string? Address { get;  set; }
}