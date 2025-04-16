namespace AdministracionApi.Security.Data;

public interface ISecurityRepository
{
    Task<AuthenticationResponse> Login(LoginInfo loginInfo,CancellationToken cancellationToken = default);
    Task<AuthenticationResponse> CreateUser(RegisterUserInfo registerUserInfo,CancellationToken cancellationToken = default);
    Task<AuthenticationResponse> UpdateUser(UserInfo userInfo,CancellationToken cancellationToken = default);
    Task<Unit> ChangePassword(string newPassword,CancellationToken cancellationToken = default);
    Task<Unit> DeleteUser(CancellationToken cancellationToken = default);
}
