using ElectraNet.Domain.Enitites.Users;

namespace ElectraNet.Service.DTOs.UserPermissions;

public class UserPermissionCreateModel
{
    public long UserId { get; set; }
    public long PermissionId { get; set; }
}