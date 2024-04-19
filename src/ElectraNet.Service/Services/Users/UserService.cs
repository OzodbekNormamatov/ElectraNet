using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.Configurations;

namespace ElectraNet.Service.Services.Users;

public class UserService : IUserService
{
    public async ValueTask<User> CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<User> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<User> UpdateAsync(long id, User user)
    {
        throw new NotImplementedException();
    }
}