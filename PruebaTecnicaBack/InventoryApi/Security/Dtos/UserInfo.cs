namespace AdministracionApi.Security.Dtos;

public class UserInfo : IUserInfo
{
    public required string UserName { get; set; } = default!;
    public required string Email { get; set; } = default!;
    public required string FirstName { get; set; } = default!;
    public required string LastName { get; set; } = default!;
}