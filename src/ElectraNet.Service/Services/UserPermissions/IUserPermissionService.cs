using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.UserPermissions;

namespace ElectraNet.Service.Services.UserPermissions;

public interface IUserPermissionService
{
    /// <summary>
    /// Deletes a user permission based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the user permission to delete.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, containing a <see cref="bool"/> indicating whether the deletion was successful or not.</returns>
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves a user permission based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the user permission to retrieve.
    ValueTask<UserPermissionViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Creates a new user permission based on the provided user permission creation model.
    /// </summary>
    /// <param name="permission">The user permission creation model containing information about the user permission to create.
    ValueTask<UserPermissionViewModel> CreateAsync(UserPermissionCreateModel permission);

    /// <summary>
    /// Updates an existing user permission based on the specified ID and the provided user permission update model.
    /// </summary>
    /// <param name="id">The ID of the user permission to update.</param>
    /// <param name="permission">The user permission update model containing the new information for the user permission.
    ValueTask<UserPermissionViewModel> UpdateAsync(long id, UserPermissionUpdateModel permission);

    /// <summary>
    /// Retrieves all user permissions based on the provided pagination parameters, filter, and optional search term.
    /// </summary>
    /// <param name="@params">The pagination parameters specifying the page size and number.</param>
    /// <param name="filter">The filter criteria for selecting user permissions.</param>
    /// <param name="search">The optional search term to filter user permissions.
    ValueTask<IEnumerable<UserPermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<IEnumerable<UserPermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
}