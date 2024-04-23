using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.UserRoles;

namespace ElectraNet.Service.Services.UserRoles;

public interface IUserRoleService
{
    ValueTask<UserRoleViewModel> CreateAsync(UserRoleCreateModel userRoleCreateModel);
    ValueTask<UserRoleViewModel> UpdateAsync(long id, UserRoleUpdateModel userRoleUpdateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserRoleViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserRoleViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}