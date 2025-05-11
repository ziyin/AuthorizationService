namespace WebService.Authorization.HttpApi.Repsonse.Role;

public class RolePermissionListResponse
{
    public IEnumerable<RolePermissionResponse> Data { get; set; } = [];
}