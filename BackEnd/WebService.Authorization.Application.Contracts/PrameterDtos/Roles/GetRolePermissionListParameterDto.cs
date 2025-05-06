namespace WebService.Authorization.Application.Contracts.PrameterDtos.Roles;

public class GetRolePermissionListParameterDto
{
    public IEnumerable<string>? RoleName { get; set; }
    public IEnumerable<Guid>? RoleId { get; set; }
}