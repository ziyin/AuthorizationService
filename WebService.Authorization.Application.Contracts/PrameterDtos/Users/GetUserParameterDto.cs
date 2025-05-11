namespace WebService.Authorization.Application.Contracts.PrameterDtos.Users;

public class GetUserParameterDto
{
    public Guid? UserId { get; set; }
    public string? Account { get; set; }
}