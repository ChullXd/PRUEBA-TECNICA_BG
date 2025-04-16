namespace AdministracionApi.Security.Endpoints;

public class SecurityRoutes
{
    public const string Tag = "Security";
    private const string BaseApi = "/api";
    private const string Base = $"{BaseApi}/security";
    public const string Login = $"{Base}/login";
    public const string CreateUser = $"{Base}/createuser";
    public const string ChangePassword = $"{Base}/changepassword";
    public const string DeleteUser = $"{Base}/deleteuser";
    public const string UpdateUser = $"{Base}/updateuser";
    
}