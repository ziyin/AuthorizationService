namespace WebService.Authorization.Application.Contracts.PrameterDtos.Permission;

public class CreatePermissionParmeterDto
{
    public string Code { get;  set; } = null!;
    public string Name { get;  set; } = null!;
    public Guid Creator { get;  set; }
}