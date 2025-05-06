namespace WebService.Authorization.HttpApi.Request.User;

public class SetUserRoleRequest
{
    public IEnumerable<Guid> RoleIds { get; set; } = null!;
}