using ElectraNet.Domain.Commons;

namespace ElectraNet.Domain.Enitites.Users;

public class UserPermission : Auditable
{
    /// <summary>
    /// Represents the unique identifier of the user.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Represents the associated user entity.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Represents the unique identifier of the permission.
    /// </summary>
    public long PermissionId { get; set; }

    /// <summary>
    /// Represents the associated permission entity.
    /// </summary>
    public Permission Permission { get; set; }
}