namespace WebService.Authorization.Application.Contracts.PrameterDtos.Roles;

public class CreateRolePermissionParameterDto
{
    public Guid RoleId { get; set; }
    public IEnumerable<Guid> Permissions { get; set; } = null!;
    public Guid Creator { get; set; }
}