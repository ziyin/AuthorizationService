namespace WebService.Authorization.Domain.User.Models.Parameters;

public class GetUserParameterModel
{
    public Guid? UserId { get; set; }
    public string? Account { get; set; }
}