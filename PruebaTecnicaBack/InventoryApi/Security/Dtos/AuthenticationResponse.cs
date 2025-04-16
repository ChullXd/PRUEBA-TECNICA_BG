namespace AdministracionApi.Security.Dtos;

public class AuthenticationResponse
{ 
    public string Token { get; set; } = default!; 
    public DateTime Expiracion { get; set; }
    
}