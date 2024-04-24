using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Permissions;

namespace ElectraNet.Service.Services.Permissions;

public interface IPermissionService
{
    /// <summary>
    /// Deletes a permission based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the permission to delete.
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves a permission asynchronously based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the permission to retrieve.
    ValueTask<PermissionViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Creates a new permission based on the provided permission creation model.
    /// </summary>
    /// <param name="permission">The permission creation model containing information about the permission to create.
    ValueTask<PermissionViewModel> CreateAsync(PermissionCreateModel permission);

    /// <summary>
    /// Updates an existing permission based on the specified ID and the provided permission update model.
    /// </summary>
    /// <param name="id">The ID of the permission to update.</param>
    /// <param name="permission">The permission update model containing the new information for the permission.</param>

    ValueTask<PermissionViewModel> UpdateAsync(long id, PermissionUpdateModel permission);

    /// <summary>
    /// Retrieves all permissions based on the provided pagination parameters, filter, and optional search term.
    /// </summary>
    /// <param name="params">The pagination parameters specifying the page size and number.</param>
    /// <param name="filter">The filter criteria for selecting permissions.</param>
    /// <param name="search">The optional search term to filter permissions.

    ValueTask<IEnumerable<PermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

}