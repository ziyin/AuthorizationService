namespace WebService.Authorization.Application.Contracts.PrameterDtos.Roles;

public class CreateRoleParameterDto
{
    public string RoleName { get; set; } = null!;

    public Guid Creator { get; set; }
}