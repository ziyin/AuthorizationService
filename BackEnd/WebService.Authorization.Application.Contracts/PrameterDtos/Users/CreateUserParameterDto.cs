namespace WebService.Authorization.Application.Contracts.PrameterDtos.Users;

public class CreateUserParameterDto
{
    public string Name { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string RegionBusinessUnit { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public Guid Creator { get; set; }
}