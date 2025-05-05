namespace WebService.Authorization.Application.Contracts.PrameterDtos.Users;

public class UpdateUserParameterDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string RegionBusinessUnit { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public Guid LastModifiedBy { get; set; }
}