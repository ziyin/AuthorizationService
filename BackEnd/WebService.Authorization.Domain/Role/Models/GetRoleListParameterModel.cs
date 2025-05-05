namespace WebService.Authorization.Domain.Role.Models;

public class GetRoleListParameterModel
{
    public IEnumerable<Guid>? RoleId { get; set; }
}