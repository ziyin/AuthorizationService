namespace WebService.Authorization.Application.Contracts.PrameterDtos.Roles;

public class SetRoleEnableParameterDto
{
    public Guid RoleId { get; set; }
    public bool Enable { get; set; } 
    public Guid LastModifiedBy { get; set; }
}