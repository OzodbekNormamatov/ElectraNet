using ElectraNet.Domain.Commons;
namespace ElectraNet.Domain.Enitites.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public long RoledId { get; set; }
    public UserRole UserRole { get; set; }
}
