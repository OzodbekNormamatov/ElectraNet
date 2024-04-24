using ElectraNet.Domain.Commons;

namespace ElectraNet.Domain.Enitites.Users;

public class UserRole : Auditable
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }
}
