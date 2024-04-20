using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Helpers;

namespace ElectraNet.Service.Users;
public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async ValueTask<User> CreateAsync(User user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => (u.Number == user.Number || u.Email == user.Email) && !u.IsDeleted);
        if (existUser is not null)
            throw new AlreadyExistException($"This user already exists with this phone={user.Number}");

        user.CreatedByUserId = HttpContextHelper.UserId;
        user.Password = PasswordHasher.Hash(user.Password);
        var createdUser = await unitOfWork.Users.InsertAsync(user);
        await unitOfWork.SaveAsync();

        return createdUser;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted)
             ?? throw new NotFoundException($"User is not found with this ID={id}");

        existUser.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Users.DeleteAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.Users.SelectAsQueryable().OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            users = users.Where(p =>
             p.Method.Contains(search, StringComparison.OrdinalIgnoreCase) ||
             p.Controller.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(users.ToPaginate(@params));
    }

    public async ValueTask<User> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted)
           ?? throw new NotFoundException($"User is not found with this ID={id}");

        return existUser;
    }

    public async ValueTask<User> UpdateAsync(long id, User user)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id)
            ?? throw new NotFoundException($"User is not found with this ID={id}");

        var alreadyExistUser = await unitOfWork.Users.SelectAsync(u => (u.Number == user.Number || u.Email == user.Email) && !u.IsDeleted);
        if (existUser is not null)
            throw new AlreadyExistException($"This user already exists with this phone={user.Number}");

        existUser.Number = user.Number;
        existUser.Password = user.Password;
        existUser.Email = user.Email;
        existUser.RoledId = user.RoledId;
        existUser.LastName = user.LastName;
        existUser.FirstName = user.FirstName;
        existUser.UpdatedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return existUser;
    }
}