namespace WebService.Authorization.Application.Contracts.PrameterDtos.Roles;

public class AddRoleParameterDto
{
    public string RoleName { get; set; } = null!;

    public string Creator { get; set; } = null!;
}