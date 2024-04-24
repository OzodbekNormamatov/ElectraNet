using ElectraNet.Service.DTOs.Users;
using ElectraNet.Service.DTOs.Permissions;

namespace ElectraNet.Service.DTOs.UserPermissions;

public class UserPermissionViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public PermissionViewModel Permission { get; set; }
}