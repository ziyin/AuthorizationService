namespace WebService.Authorization.Domain.User.Models;

public class UserEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Account { get; private set; }
    /// <summary>
    /// Hash
    /// </summary>
    public string Password { get; private set; }
    public string RegionBusinessUnit { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }
    public DateTime CreateTime { get; private set; }
    public Guid Creator { get; private set; }
    public bool Enable { get; private set; }
    public DateTime? LastModified { get; private set; }
    public Guid? LastModifiedBy { get; private set; }

    private UserEntity(
        Guid id,
        string name,
        string account,
        string password,
        string regionBusinessUnit,
        string? email,
        string? phone,
        string? address,
        DateTime createTime,
        Guid creator,
        bool enable,
        DateTime? lastModified,
        Guid? lastModifiedBy
        )
    {
        Id = id;
        Name = name;
        Account = account;
        Password = password;
        RegionBusinessUnit = regionBusinessUnit;
        Email = email;
        Phone = phone;
        Address = address;
        CreateTime = createTime;
        Creator = creator;
        Enable = enable;
        LastModified = lastModified;
        LastModifiedBy = lastModifiedBy;
    }

    public static UserEntity Create(
        string name,
        string account,
        string hashedPassword,
        string regionBusinessUnit,
        string? email,
        string? phone,
        string? address,
        Guid creator)
    {
        return new UserEntity(
            Guid.Empty,
            name,
            account,
            hashedPassword,
            regionBusinessUnit,
            email,
            phone,
            address,
            DateTime.UtcNow,
            creator,
            enable: true,
            null,
            null
        );
    }

    public void Update(
        string name,
        string regionBusinessUnit,
        string? email,
        string? phone,
        string? address,
        Guid modifiedBy
        )
    {
        Name = name;
        RegionBusinessUnit = regionBusinessUnit;
        Email = email;
        Phone = phone;
        Address = address;
        LastModifiedBy = modifiedBy;
        LastModified = DateTime.UtcNow;
    }

    public void ResetPassword
        (
        string resetPassword,
        Guid modifiedBy
        )
    {
        Password = resetPassword;
        LastModifiedBy = modifiedBy;
        LastModified = DateTime.UtcNow;
    }

    public void SetEnableState
        (
        bool enable,
        Guid modifiedBy
        )
    {
        Enable = enable;
        LastModifiedBy = modifiedBy;
        LastModified = DateTime.UtcNow;
    }
}