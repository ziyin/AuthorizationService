namespace WebService.Authorization.HttpApi.Repsonse.Role;

public class RolePermissionResponse
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public string PermissionCode { get; set; } = null!;
    public string PermissionName { get; set; } = null!;
}