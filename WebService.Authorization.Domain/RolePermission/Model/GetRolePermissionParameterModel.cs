namespace WebService.Authorization.Domain.RolePermission.Model;

public class GetRolePermissionParameterModel
{
    public List<Guid> RoleIds { get; set; } = [];
    public IEnumerable<string>? RoleNames { get; set; }
}