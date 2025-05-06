namespace WebService.Authorization.Application.Contracts.PrameterDtos.Roles;

public class GetRoleParameterDto
{
    public Guid? RoleId { get; set; }
    public string? RoleName { get; set; }
}