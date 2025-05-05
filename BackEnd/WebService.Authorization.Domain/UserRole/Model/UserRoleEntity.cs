namespace WebService.Authorization.Domain.UserRole.Model;

public class UserRoleEntity
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public Guid RoleId { get; private set; }

    public string RoleName { get; private set; }

    public DateTime CreateTime { get; private set; }

    public Guid Creator { get; private set; }


    private UserRoleEntity() { }

    public UserRoleEntity
        (
        Guid userId,
        Guid roleId,
        Guid creator
        )
    {
        Id = Guid.NewGuid();
        UserId = userId;
        RoleId = roleId;
        Creator = creator;
        CreateTime = DateTime.UtcNow;
    }

    public static UserRoleEntity Create
        (
        Guid userId,
        Guid roleId,
        Guid creator
        )
    {
        return new UserRoleEntity(userId, roleId, creator);
    }
}