namespace AdministracionApi.Security.Dtos;

public class RegisterUserInfo : LoginInfo, IUserInfo
{
    public required string FirstName { get; set; } = default!;
    public required string LastName { get; set; } = default!;
    public required string Email { get; set; } = default!;
}