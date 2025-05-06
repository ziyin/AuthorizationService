namespace WebService.Authorization.Domain.RolePermission.Model;

public class RolePermissionEntity
{
    public Guid Id { get; private set; }
    public Guid RoleId { get; private set; }
    public Guid PermissionId { get; private set; }
    public string PermissionCode { get; private set; }
    public string PermissionName { get; private set; }
    public DateTime CreateTime { get; private set; }
    public Guid Creator { get; private set; }

    public RolePermissionEntity() { }

    public RolePermissionEntity
        (
        Guid roleId,
        Guid permissionId,
        Guid creator
        )
    {
        Id = Guid.NewGuid();
        RoleId = roleId;
        PermissionId = permissionId;
        Creator = creator;
        CreateTime = DateTime.UtcNow;
    }

    public static RolePermissionEntity Create
        (
        Guid roleId,
        Guid permissionId,
        Guid creator
        )
    {
        return new RolePermissionEntity(roleId, permissionId, creator);
    }

    public static IEnumerable<RolePermissionEntity> CreateManyPermissions
    (
    Guid roleId,
    IEnumerable<Guid> permissionIds,
    Guid creator
    )
    {
        return permissionIds.Select(item => Create(roleId, item, creator));
    }
}
