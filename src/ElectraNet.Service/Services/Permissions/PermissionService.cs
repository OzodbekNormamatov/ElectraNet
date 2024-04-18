using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using ElectraNet.Service.Helpers;

namespace ElectraNet.Service.Services.Permissions;

public class PermissionService(IUnitOfWork unitOfWork) : IPermissionService
{
    public async ValueTask<Permission> CreateAsync(Permission permission)
    {
        var existPermission = await unitOfWork.Permissions.SelectAsync(p =>
            p.Method.ToLower() == permission.Method.ToLower() &&
            p.Controller.ToLower() == permission.Controller.ToLower());

        if (existPermission is not null)
            throw new AlreadyExistException($"This permission is already exists | Method = {permission.Method} Controller = {permission.Controller}");

        permission.CreatedByUserId = HttpContextHelper.UserId;
        var createdPermission = await unitOfWork.Permissions.InsertAsync(permission);
        await unitOfWork.SaveAsync();

        return createdPermission;
    }

    public async ValueTask<Permission> UpdateAsync(long id, Permission permission)
    {
        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this ID = {id}");

        var alreadyExistPermission = await unitOfWork.Permissions.SelectAsync(p =>
             p.Method.ToLower() == permission.Method.ToLower() &&
             p.Controller.ToLower() == permission.Controller.ToLower());

        if (alreadyExistPermission is not null)
            throw new AlreadyExistException($"This permission is already exists | Method = {permission.Method} Controller = {permission.Controller}");

        existPermission.Method = permission.Method;
        existPermission.Controller = permission.Controller;
        existPermission.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Permissions.UpdateAsync(permission);
        await unitOfWork.SaveAsync();

        return existPermission;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this ID = {id}");

        await unitOfWork.Permissions.DropAsync(existPermission);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<Permission> GetByIdAsync(long id)
    {
        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Permission is not found with this ID = {id}");

        return existPermission;
    }

    public async ValueTask<IEnumerable<Permission>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var permissions = unitOfWork.Permissions.SelectAsQueryable().OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            permissions = permissions.Where(p =>
             p.Method.Contains(search, StringComparison.OrdinalIgnoreCase) ||
             p.Controller.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(permissions.ToPaginate(@params));
    }
}