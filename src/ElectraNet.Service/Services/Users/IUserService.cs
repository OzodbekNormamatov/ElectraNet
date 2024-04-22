using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Users;

namespace ElectraNet.Service.Services.Users;

public interface IUserService
{
    ValueTask<UserViewModel> CreateAsync(UserCreateModel createModel);
    ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}