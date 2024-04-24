using AutoMapper;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Permissions;
using ElectraNet.Service.Validators.Permissions;

namespace ElectraNet.Service.Services.Permissions;

public class PermissionService(
    IMapper mapper, 
    IUnitOfWork unitOfWork,
    PermissionCreateModelValidator permissionCreateModelValidator,
    PermissionUpdateModelValidator permissionUpdateModelValidator) : IPermissionService
{
    public async ValueTask<PermissionViewModel> CreateAsync(PermissionCreateModel createModel)
    {
        var validator = await permissionCreateModelValidator.ValidateAsync(createModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existPermission = await unitOfWork.Permissions.SelectAsync(p =>
            p.Method.ToLower() == createModel.Method.ToLower() &&
            p.Controller.ToLower() == createModel.Controller.ToLower());

        if (existPermission is not null)
            throw new AlreadyExistException($"This permission is already exists | Method = {createModel.Method} Controller = {createModel.Controller}");

        var permission = mapper.Map<Permission>(createModel);
        permission.Create();
        var createdPermission = await unitOfWork.Permissions.InsertAsync(permission);
        await unitOfWork.SaveAsync();

        return mapper.Map<PermissionViewModel>(createdPermission);
    }

    public async ValueTask<PermissionViewModel> UpdateAsync(long id, PermissionUpdateModel updateModel)
    {
        var validator = await permissionUpdateModelValidator.ValidateAsync(updateModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this ID = {id}");

        var alreadyExistPermission = await unitOfWork.Permissions.SelectAsync(p =>
             p.Method.ToLower() == updateModel.Method.ToLower() &&
             p.Controller.ToLower() == updateModel.Controller.ToLower());

        if (alreadyExistPermission is not null)
            throw new AlreadyExistException($"This permission is already exists | Method = {updateModel.Method} Controller = {updateModel.Controller}");

        mapper.Map(updateModel, existPermission); 
        existPermission.Update();
        await unitOfWork.Permissions.UpdateAsync(existPermission);
        await unitOfWork.SaveAsync();

        return mapper.Map<PermissionViewModel>(existPermission);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this ID = {id}");

        await unitOfWork.Permissions.DropAsync(existPermission);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<PermissionViewModel> GetByIdAsync(long id)
    {
        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this ID = {id}");

        return mapper.Map<PermissionViewModel>(existPermission);
    }

    public async ValueTask<IEnumerable<PermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var permissions = unitOfWork.Permissions.SelectAsQueryable().OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            permissions = permissions.Where(p =>
             p.Method.ToLower().Contains(search.ToLower()) ||
             p.Controller.ToLower().Contains(search.ToLower()));

        var paginatePermission = await permissions.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<PermissionViewModel>>(paginatePermission);
    }
}