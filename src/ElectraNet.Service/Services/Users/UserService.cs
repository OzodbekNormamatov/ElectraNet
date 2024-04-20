using AutoMapper;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Users;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ElectraNet.Service.Services.Users;

public class UserService(IMapper mapper, IUnitOfWork unitOfWork) : IUserService
{
    public async ValueTask<UserViewModel> CreateAsync(UserCreateModel createModel)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => (u.Number == createModel.Number || u.Email == createModel.Email));

        if (existUser is not null)
            throw new AlreadyExistException($"User is already exist{createModel.Number}");

        var userRole = mapper.Map<UserRole>(createModel);
        userRole.Create();
        var createdUserRole = await unitOfWork.UserRoles.InsertAsync(userRole);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(createdUserRole);
    }
    public async ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel updateModel)
    {
        var existUser = await unitOfWork.Users.SelectAsync(p => p.Id == id)
           ?? throw new NotFoundException($"User is not found with this ID = {id}");

        var alreadyExistPermission = await unitOfWork.Users.SelectAsync(u => (u.Number == updateModel.Number || u.Email == updateModel.Email));

        if (alreadyExistPermission is not null)
            throw new AlreadyExistException($"This user is already exists with | Number = {updateModel.Number} Email = {updateModel.Email}");
        existUser.Password = updateModel.Password;
        existUser.FirstName = updateModel.FirstName;
        existUser.LastName = updateModel.LastName;
        existUser.Email = updateModel.Email;
        existUser.Number = updateModel.Number;
        existUser.RoledId = updateModel.RoledId;
        existUser.Create();
        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(existUser);
    }
    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"User is not found with this ID = {id}");

        await unitOfWork.Users.DropAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.Users.SelectAsQueryable().OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            users = users.Where(p =>
             p.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
             p.LastName.Contains(search, StringComparison.OrdinalIgnoreCase));

        return mapper.Map<IEnumerable<UserViewModel>>(await users.ToPaginateAsQueryable(@params).ToListAsync());
    }
    public async ValueTask<UserViewModel> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"User is not found with this ID = {id}");

        return mapper.Map<UserViewModel>(existUser);
    }
}

