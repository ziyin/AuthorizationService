namespace WebService.Authorization.HttpApi.Request.User;

public class GetUserListRequest
{
    public string? Name { get; set; }
    public string? Account { get; set; }
    public string? RegionBusinessUnit { get; set; }
    public bool? Enable { get; set; }
}