using AutoMapper;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.UserRoles;
using ElectraNet.WebApi.Validator.UserRolesl;
using ElectraNet.WebApi.Validator.UserRoles;

namespace ElectraNet.Service.Services.UserRoles;

public class UserRoleService(IMapper mapper, 
    IUnitOfWork unitOfWork, 
    UserRoleCreateModelValidator userRoleCreateValidator,
    UserRoleUpdateModelValidator userRoleUpdateValidator) : IUserRoleService
{
    public async ValueTask<UserRoleViewModel> CreateAsync(UserRoleCreateModel userRoleCreateModel)
    {
        var validator = await userRoleCreateValidator.ValidateAsync(userRoleCreateModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existUserRole = await unitOfWork.UserRoles.SelectAsync(u => u.Name.ToLower() == userRoleCreateModel.Name.ToLower());

        if (existUserRole is not null)
            throw new AlreadyExistException("UserRole is already exist");

        var userRole = mapper.Map<UserRole>(userRoleCreateModel);
        userRole.Create();
        var createdUserRole = await unitOfWork.UserRoles.InsertAsync(userRole);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserRoleViewModel>(createdUserRole);
    }

    public async ValueTask<UserRoleViewModel> UpdateAsync(long id, UserRoleUpdateModel userRoleUpdateModel)
    {
        var validator = await userRoleUpdateValidator.ValidateAsync(userRoleUpdateModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existUserRole = await unitOfWork.UserRoles.SelectAsync(u => u.Id == id && !u.IsDeleted)
            ?? throw new NotFoundException("UserRole is not found");

        var alreadyExistUserRole = await unitOfWork.UserRoles.SelectAsync(u => u.Name.ToLower() == userRoleUpdateModel.Name.ToLower());
        if (alreadyExistUserRole is not null)
            throw new AlreadyExistException("UserRole is already exist");

        mapper.Map(userRoleUpdateModel , existUserRole);
        existUserRole.Update();
        var updateUserRole = await unitOfWork.UserRoles.UpdateAsync(existUserRole);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserRoleViewModel>(updateUserRole);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.UserRoles.SelectAsync(u => u.Id == id)
           ?? throw new NotFoundException("UserRole is not found");

        existUser.Delete();
        await unitOfWork.UserRoles.DropAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<UserRoleViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var userRoles = unitOfWork.UserRoles.SelectAsQueryable(isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            userRoles = userRoles.Where((role => role.Name.ToLower().Contains(search.ToLower())));

        var paginateUserRoles = await userRoles.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<UserRoleViewModel>>(paginateUserRoles);
    }

    public async ValueTask<UserRoleViewModel> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.UserRoles.SelectAsync(u => u.Id == id && !u.IsDeleted)
           ?? throw new NotFoundException("UserRole is not found");

        return mapper.Map<UserRoleViewModel>(existUser);
    }
}