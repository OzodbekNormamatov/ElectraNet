using ElectraNet.Domain.Commons;
namespace ElectraNet.Domain.Enitites.Users;
public class User : Auditable
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Gets or sets the contact number of the user.
    /// </summary>
    public string Number { get; set; }
    /// <summary>
    /// Gets or sets the ID of the role associated with the user.
    /// </summary>
    public long RoledId { get; set; }
    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public UserRole UserRole { get; set; }
}
