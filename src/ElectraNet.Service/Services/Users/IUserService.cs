using ElectraNet.Service.DTOs.Users;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.Configurations;

namespace ElectraNet.Service.Services.Users;

public interface IUserService
{
    ValueTask<UserViewModel> CreateAsync(UserCreateModel createModel);
    ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel updateModel, bool IsUserDeleted = false);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<(UserViewModel user, string token)> LoginAsync(string phone, string password);
    ValueTask<bool> ResetPasswordAsync(string phone, string newPassword);
    ValueTask<bool> SendCodeAsync(string phone);
    ValueTask<bool> ConfirmCodeAsync(string phone, string code);
    ValueTask<User> ChangePasswordAsync(string phone, string oldPassword, string newPassword);
}