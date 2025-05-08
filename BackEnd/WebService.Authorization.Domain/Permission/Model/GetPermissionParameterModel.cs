namespace WebService.Authorization.Domain.Permission.Model;

public class GetPermissionParameterModel
{
    public Guid? PermissionId { get; set; }
    public string? PermissionCode { get; set; }
}