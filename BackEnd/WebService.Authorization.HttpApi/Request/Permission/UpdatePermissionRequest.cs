namespace WebService.Authorization.HttpApi.Request.Permission;

public class UpdatePermissionRequest
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}