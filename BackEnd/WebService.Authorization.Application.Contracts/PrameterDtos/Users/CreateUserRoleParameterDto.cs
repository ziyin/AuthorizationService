namespace WebService.Authorization.Application.Contracts.PrameterDtos.Users;

public class CreateUserRoleParameterDto
{
    public Guid UserId { get; set; }
    public IEnumerable<Guid> RoleIds { get; set; } = null!;
    public Guid Creator { get; set; }
}