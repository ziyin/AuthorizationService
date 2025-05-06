namespace WebService.Authorization.Domain.Permission.Model;

public class GetPermissionListParameterModel
{
    public IEnumerable<Guid>? PermissionId { get; set; }
}