namespace WebService.Authorization.HttpApi.Request.Role;

public class SetRolePermissionRequest
{
    public IEnumerable<Guid> Permissions { get; set; } = null!;
}