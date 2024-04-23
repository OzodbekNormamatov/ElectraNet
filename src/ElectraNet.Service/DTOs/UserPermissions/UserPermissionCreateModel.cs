using ElectraNet.Domain.Enitites.Users;

namespace ElectraNet.Service.DTOs.UserPermissions;

public class UserPermissionCreateModel
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long PermissionId { get; set; }
    public Permission Permission { get; set; }
}