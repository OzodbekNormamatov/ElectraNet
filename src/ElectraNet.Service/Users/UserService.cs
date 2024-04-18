using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectraNet.Service.Users;

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
