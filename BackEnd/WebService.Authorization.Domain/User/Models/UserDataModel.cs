namespace WebService.Authorization.Domain.User.Models;

public class UserDataModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string RegionBusinessUnit { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateTime CreateTime { get; set; }

    public Guid Creator { get; set; }
}