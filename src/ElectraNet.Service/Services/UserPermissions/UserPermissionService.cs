using AutoMapper;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.Configurations;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.DTOs.UserPermissions;

namespace ElectraNet.Service.Services.UserPermissions
{
    public class UserPermissionService(IMapper mapper, IUnitOfWork unitOfWork) : IUserPermissionService
    {
        public async ValueTask<UserPermissionViewModel> CreateAsync(UserPermissionCreateModel createModel)
        {
            var existPermission = await unitOfWork.UserPermissions
                .SelectAsync(p => p.UserId == createModel.UserId && p.PermissionId == createModel.PermissionId);

            if (existPermission is not null)
                throw new AlreadyExistException($"This user permission already exists | UserId = {createModel.UserId} PermissionId = {createModel.PermissionId}");

            var permission = mapper.Map<UserPermission>(createModel);
            existPermission.Create();
            var createdPermission = await unitOfWork.UserPermissions.InsertAsync(permission);
            await unitOfWork.SaveAsync();

            return mapper.Map<UserPermissionViewModel>(createdPermission);
        }

        public async ValueTask<UserPermissionViewModel> UpdateAsync(long id, UserPermissionUpdateModel updateModel)
        {
            var existPermission = await unitOfWork.UserPermissions.SelectAsync(p => p.Id == id && !p.IsDeleted)
                ?? throw new NotFoundException($"User permission is not found with this ID = {id}");

            var alreadyExistUserPermission = await unitOfWork.UserPermissions.SelectAsync(u => u.UserId == u.PermissionId);

            if (alreadyExistUserPermission is not null)
                throw new AlreadyExistException($"This user permission is already exists | UserId = {updateModel.UserId} PermissionId = {updateModel.PermissionId}");

            mapper.Map(updateModel, existPermission);
            existPermission.Update();
            await unitOfWork.UserPermissions.UpdateAsync(existPermission);
            await unitOfWork.SaveAsync();

            return mapper.Map<UserPermissionViewModel>(existPermission);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var existPermission = await unitOfWork.UserPermissions.SelectAsync(p => p.Id == id)
                ?? throw new NotFoundException($"User permission not found with ID = {id}");

            existPermission.Delete();
            await unitOfWork.UserPermissions.DropAsync(existPermission);
            await unitOfWork.SaveAsync();

            return true;
        }

        public async ValueTask<UserPermissionViewModel> GetByIdAsync(long id)
        {
            var existPermission = await unitOfWork.UserPermissions.SelectAsync(p => p.Id == id && !p.IsDeleted)
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

            return mapper.Map<IEnumerable<UserPermissionViewModel>>(permissions);
        }
    }
}
