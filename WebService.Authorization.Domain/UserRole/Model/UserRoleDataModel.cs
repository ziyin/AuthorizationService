namespace WebService.Authorization.Domain.UserRole.Model;

public class UserRoleDataModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? UserName { get; set; }

    public Guid RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public Guid Creator { get; set; }
}
