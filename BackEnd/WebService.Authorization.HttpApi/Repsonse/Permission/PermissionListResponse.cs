namespace WebService.Authorization.HttpApi.Repsonse.Permission;

public class PermissionListResponse
{
    public IEnumerable<PermissionResponse> List { get; set; } = [];
}