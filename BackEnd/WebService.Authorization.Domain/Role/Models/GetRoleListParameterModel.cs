namespace WebService.Authorization.Domain.Role.Models;

public class GetRoleListParameterModel
{
    public IEnumerable<Guid>? RoleId { get; set; }
    public IEnumerable<string>? RoleName { get; set; }
    public bool Enable { get; set; } = true;
}