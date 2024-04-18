using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.Configurations;

namespace ElectraNet.Service.Services.Permissions;

public interface IPermissionService
{
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Permission> GetByIdAsync(long id);
    ValueTask<Permission> CreateAsync(Permission permission);
    ValueTask<Permission> UpdateAsync(long id, Permission permission);
    ValueTask<IEnumerable<Permission>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}