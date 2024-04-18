using ElectraNet.Domain.Commons;

namespace ElectraNet.Domain.Enitites.Users;

public class UserPermission : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long PermissionId { get; set; }
    public Permission Permission { get; set; }
}