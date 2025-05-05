namespace WebService.Authorization.Domain.Role.Models;

public class RoleDataModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Enable { get; set; }
    public DateTime CreateTime { get; set; }
    public Guid Creator { get; set; }
}
