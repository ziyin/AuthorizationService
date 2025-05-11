namespace WebService.Authorization.Application.Contracts.PrameterDtos.Permission;

public class UpdatePermissionParamterDto
{
    public Guid PermissionId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Guid LastModifiedBy { get; set; }
}