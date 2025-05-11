namespace WebService.Authorization.Domain.User.Models.Parameters;

public class GetUserListParameterModel
{
    public string? Name { get; set; }
    public string? Account { get; set; }
    public string? RegionBusinessUnit { get; set; }
    public bool? Enable { get; set; }
}