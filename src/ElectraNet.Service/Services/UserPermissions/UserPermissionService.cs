using AutoMapper;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.UserPermissions;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ElectraNet.Service.Services.UserPermissions
{
    public class UserPermissionService(IMapper mapper, IUnitOfWork unitOfWork) : IUserPermissionService
    {
        public async ValueTask<UserPermissionViewModel> CreateAsync(UserPermissionCreateModel permission)
        {
            var existPermission = await unitOfWork.UserPermissions
                .SelectAsync(p => p.UserId == permission.UserId && p.PermissionId == permission.PermissionId);

            if (existPermission is not null)
                throw new AlreadyExistException($"This user permission already exists | UserId = {permission.UserId} PermissionId = {permission.PermissionId}");

            existPermission.Create();
            var createdPermission = await unitOfWork.UserPermissions.InsertAsync(existPermission);
            await unitOfWork.SaveAsync();

            return mapper.Map<UserPermissionViewModel>(createdPermission);
        }

        public async ValueTask<UserPermissionViewModel> UpdateAsync(long id, UserPermissionUpdateModel permission)
        {
            var existPermission = await unitOfWork.UserPermissions.SelectAsync(p => p.Id == id)
                ?? throw new NotFoundException($"User permission is not found with this ID = {id}");

            var alreadyExistUserPermission = await unitOfWork.UserPermissions.SelectAsync(u => u.UserId == u.PermissionId);

            if (alreadyExistUserPermission is not null)
                throw new AlreadyExistException($"This user permission is already exists | UserId = {permission.UserId} PermissionId = {permission.PermissionId}");

            existPermission.PermissionId = permission.PermissionId;
            existPermission.UserId = permission.UserId;

            existPermission.Create();
            await unitOfWork.UserPermissions.UpdateAsync(existPermission);
            await unitOfWork.SaveAsync();

            return mapper.Map<UserPermissionViewModel>(existPermission);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var existPermission = await unitOfWork.UserPermissions.SelectAsync(p => p.Id == id)
                ?? throw new NotFoundException($"User permission not found with ID = {id}");

            await unitOfWork.UserPermissions.DropAsync(existPermission);
            await unitOfWork.SaveAsync();

            return true;
        }

        public async ValueTask<UserPermissionViewModel> GetByIdAsync(long id)
        {
            var existPermission = await unitOfWork.UserPermissions.SelectAsync(p => p.Id == id)
                ?? throw new NotFoundException($"User permission not found with ID = {id}");

            return mapper.Map<UserPermissionViewModel>(existPermission);
        }

        public async ValueTask<IEnumerable<UserPermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
        {
            var permissions = unitOfWork.UserPermissions.SelectAsQueryable().OrderBy(filter);

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
}
