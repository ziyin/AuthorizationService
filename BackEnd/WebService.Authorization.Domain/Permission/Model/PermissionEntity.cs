namespace WebService.Authorization.Domain.Permission.Model;

public class PermissionEntity
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public bool Enable { get; private set; }
    public DateTime CreateTime { get; private set; }
    public Guid Creator { get; private set; }
    public DateTime? LastModified { get; private set; }
    public Guid? LastModifiedBy { get; private set; }

    public PermissionEntity() { }

    public PermissionEntity
        (
        string code,
        string name,
        Guid creator,
        bool enable
        )
    {
        Code = code;
        Name = name;
        Creator = creator;
        Enable = enable;
    }

    public static PermissionEntity Create(
        string code,
        string name,
        Guid creator
        )
    {
        return new PermissionEntity(
            code,
            name,
            creator,
            enable: true
        );
    }

}