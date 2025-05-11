namespace WebService.Authorization.Application.Contracts.PrameterDtos.Users;

public class GetUserListParameterDto
{
    public string? Name { get; set; }
    public string? Account { get; set; }
    public string? RegionBusinessUnit { get; set; }
    public bool? Enable { get; set; }
}