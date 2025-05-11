namespace WebService.Authorization.Application.Contracts.PrameterDtos.Permission;

public class SetEnalbeParameterDto
{
    public Guid PermissionId { get; set; }
    public bool Enable { get; set; }
    public Guid LastModifiedBy { get; set; }
}