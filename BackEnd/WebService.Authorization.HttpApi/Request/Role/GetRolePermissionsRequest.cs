namespace WebService.Authorization.HttpApi.Request.Role;

public class GetRolePermissionsRequest
{
    public IEnumerable<string>? RoleName { get; set; }
    public IEnumerable<Guid>? RoleId { get; set; }
}