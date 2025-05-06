namespace WebService.Authorization.Domain.RolePermission.Model;

public class GetRolePermissionListParameterModel
{
    public IEnumerable<Guid>? RoleId { get; set; }
    public Guid? Permission { get; set; }
    public string? Code { get; set; }
}