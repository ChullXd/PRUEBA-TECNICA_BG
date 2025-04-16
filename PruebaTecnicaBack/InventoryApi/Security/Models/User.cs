namespace AdministracionApi.Security.Models;

[Table(nameof(User), Schema ="SEG")]
public class User : IdentityUser, IEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public static User Create(
        string firstName,
        string lastName,
        string userName,
        string email) =>
          new()
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Email = email,
        };
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool? Active { get; set; }
}