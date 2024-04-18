
using ElectraNet.Domain.Enitites.Users;

namespace ElectraNet.Service.Users;

public interface IUserService
{
    ValueTask<User> CreateAsync(User user);
    ValueTask<User> UpdateAsync(long id, User user);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<User> GetByIdAsync(long id);
    ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
