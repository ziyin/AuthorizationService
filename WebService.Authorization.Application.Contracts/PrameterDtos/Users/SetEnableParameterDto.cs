namespace WebService.Authorization.Application.Contracts.PrameterDtos.Users;

public class SetEnableParameterDto
{
    public Guid UserId { get; set; }
    public bool Enable { get; set; }
    public Guid LastModifiedBy { get; set; }
}