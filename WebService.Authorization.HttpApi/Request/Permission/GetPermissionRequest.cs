namespace WebService.Authorization.HttpApi.Request.Permission;

public class GetPermissionRequest
{
    public Guid? PermissionId { get; set; }
    public string? PermissionCode { get; set; }
}