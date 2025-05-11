namespace WebService.Authorization.HttpApi.Repsonse.Role;

public class RoleListResponse
{
    public IEnumerable<RoleResponse> List { get; set; } = [];
}