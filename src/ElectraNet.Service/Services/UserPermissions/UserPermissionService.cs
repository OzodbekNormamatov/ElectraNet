using AutoMapper;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.Configurations;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.DTOs.UserPermissions;
using ElectraNet.WebApi.Validator.UserPermissions;
using ElectraNet.Service.Services.Users;
using ElectraNet.Service.Services.Permissions;

namespace ElectraNet.Service.Services.UserPermissions;

public class UserPermissionService(
    IMapper mapper, 
    IUnitOfWork unitOfWork,
    IUserService userService,
    IPermissionService permissionService,
    UserPermissionCreateModelValidator userPermissionCreateValidator,
    UserPermissionUpdateModelValidator userPermissionUpdateValidator) : IUserPermissionService
{
    public async ValueTask<UserPermissionViewModel> CreateAsync(UserPermissionCreateModel createModel)
    {
        var validator = await userPermissionCreateValidator.ValidateAsync(createModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

     
        var existUser = await userService.GetByIdAsync(createModel.UserId);
        var existPermission = await permissionService.GetByIdAsync(createModel.PermissionId);

        var existUserPermission = await unitOfWork.UserPermissions
            .SelectAsync(p => p.UserId == createModel.UserId && p.PermissionId == createModel.PermissionId);

        if (existUserPermission is not null)
            throw new AlreadyExistException($"This user permission already exists | UserId = {createModel.UserId} PermissionId = {createModel.PermissionId}");

        var userPermission = mapper.Map<UserPermission>(createModel);
        userPermission.Create();
        var createdPermission = await unitOfWork.UserPermissions.InsertAsync(userPermission);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<UserPermissionViewModel>(createdPermission);
        viewModel.User = existUser;
        viewModel.Permission = existPermission;
        return viewModel;
    }

    public async ValueTask<UserPermissionViewModel> UpdateAsync(long id, UserPermissionUpdateModel updateModel)
    {
        var validator = await userPermissionUpdateValidator.ValidateAsync(updateModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existUser = await userService.GetByIdAsync(updateModel.UserId);
        var existPermission = await permissionService.GetByIdAsync(updateModel.PermissionId);

        var existUserPermission = await unitOfWork.UserPermissions.SelectAsync(p => p.Id == id && !p.IsDeleted)
                  ?? throw new NotFoundException($"User permission is not found with this ID = {id}");

        var alreadyExistUserPermission = await unitOfWork.UserPermissions.SelectAsync(p => p.UserId == updateModel.UserId && p.PermissionId == updateModel.PermissionId && !p.IsDeleted);
        if (alreadyExistUserPermission is not null)
            throw new AlreadyExistException($"This user permission is already exists | UserId = {updateModel.UserId} PermissionId = {updateModel.PermissionId}");

        mapper.Map(updateModel, existUserPermission);
        existUserPermission.Update();
        var updateUserPermission = await unitOfWork.UserPermissions.UpdateAsync(existUserPermission);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<UserPermissionViewModel>(updateUserPermission);

        viewModel.User = existUser;
        viewModel.Permission = existPermission;
        return viewModel;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existPermission = await unitOfWork.UserPermissions.SelectAsync(p => p.Id == id && !p.IsDeleted)
            ?? throw new NotFoundException($"User permission not found with ID = {id}");

        await unitOfWork.UserPermissions.DropAsync(existPermission);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<UserPermissionViewModel> GetByIdAsync(long id)
    {
        var existPermission = await unitOfWork.UserPermissions.SelectAsync( expression:p => p.Id == id && !p.IsDeleted, includes: ["User", "Permission"])
            ?? throw new NotFoundException($"User permission not found with ID = {id}");

        return mapper.Map<UserPermissionViewModel>(existPermission);
    }

    public async ValueTask<IEnumerable<UserPermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var permissions = unitOfWork.UserPermissions.
            SelectAsQueryable(includes: ["User","Permission"], isTracked:false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
        {
            permissions = permissions.Where(p =>
                p.User.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.User.Email.Equals(search) ||
                p.User.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.Permission.Controller.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        var paginatedPermissions = await permissions.ToPaginateAsQueryable(@params).ToListAsync();

        return mapper.Map<IEnumerable<UserPermissionViewModel>>(paginatedPermissions);
    }
}