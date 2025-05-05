namespace WebService.Authorization.Domain.UserRole.Model.Parameter;

public class GetUserRoleListParameterModel
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}