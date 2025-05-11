namespace WebService.Authorization.Application.Contracts.PrameterDtos.Users;

public class ResetPasswordParameterDto
{
    public Guid UserId { get; set; }
    public string Password { get; set; } = null!;
    public Guid LastModifiedBy { get; set; }
}