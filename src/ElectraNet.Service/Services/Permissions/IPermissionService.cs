using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Permissions;

namespace ElectraNet.Service.Services.Permissions;

public interface IPermissionService
{
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<PermissionViewModel> GetByIdAsync(long id);
    ValueTask<PermissionViewModel> CreateAsync(PermissionCreateModel permission);
    ValueTask<PermissionViewModel> UpdateAsync(long id, PermissionUpdateModel permission);
    ValueTask<IEnumerable<PermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}