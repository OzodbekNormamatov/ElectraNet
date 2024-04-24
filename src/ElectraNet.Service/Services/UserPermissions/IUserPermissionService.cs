using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.UserPermissions;

namespace ElectraNet.Service.Services.UserPermissions;

public interface IUserPermissionService
{
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserPermissionViewModel> GetByIdAsync(long id);
    ValueTask<UserPermissionViewModel> CreateAsync(UserPermissionCreateModel permission);
    ValueTask<UserPermissionViewModel> UpdateAsync(long id, UserPermissionUpdateModel permission);
    ValueTask<IEnumerable<UserPermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
}