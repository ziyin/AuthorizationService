namespace WebService.Authorization.Application.Contracts.PrameterDtos.Roles;

public class UpdateRoleParameterDto
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = null!;
    public Guid LastModifiedBy { get; set; }
}