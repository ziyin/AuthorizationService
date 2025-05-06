namespace WebService.Authorization.Application.Contracts.PrameterDtos.Roles;

public class GetRoleListParameterDto
{
    public IEnumerable<Guid>? RoleId { get; set; }
    public IEnumerable<string>? RoleName { get; set; }
}