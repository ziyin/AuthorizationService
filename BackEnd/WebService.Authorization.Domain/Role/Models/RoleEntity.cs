namespace WebService.Authorization.Domain.Role.Models;

public class RoleEntity
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public bool Enable { get; private set; }

    public DateTime CreateTime { get; private set; }

    public Guid Creator { get; private set; }

    public RoleEntity() { }

    public RoleEntity(Guid id, string name, bool enable, DateTime createTime, Guid creator)
    {
        Id = id;
        Name = name;
        Enable = enable;
        CreateTime = createTime;
        Creator = creator;
    }

    public static RoleEntity Create(
    string name,
    Guid creator)
    {
        return new RoleEntity(
            Guid.Empty,
            name,
            enable: true,
            DateTime.UtcNow,
            creator
        );
    }

    public void EnableRole() => Enable = true;

    public void DisableRole() => Enable = false;
}