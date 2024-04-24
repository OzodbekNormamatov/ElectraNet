using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.UserRoles;

namespace ElectraNet.Service.Services.UserRoles;

public interface IUserRoleService
{
    /// <summary>
    /// Creates a new user role based on the provided user role creation model.
    /// </summary>
    /// <param name="userRoleCreateModel">The user role creation model containing information about the user role to create.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, containing a <see cref="UserRoleViewModel"/> representing the created user role.</returns>
    ValueTask<UserRoleViewModel> CreateAsync(UserRoleCreateModel userRoleCreateModel);

    /// <summary>
    /// Updates an existing user role based on the specified ID and the provided user role update model.
    /// </summary>
    /// <param name="id">The ID of the user role to update.</param>
    /// <param name="userRoleUpdateModel">The user role update model containing the new information for the user role.
    ValueTask<UserRoleViewModel> UpdateAsync(long id, UserRoleUpdateModel userRoleUpdateModel);

    /// <summary>
    /// Deletes a user role based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the user role to delete.
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves a user role based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the user role to retrieve.
    ValueTask<UserRoleViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Retrieves all user roles based on the provided pagination parameters, filter, and optional search term.
    /// </summary>
    /// <param name="@params">The pagination parameters specifying the page size and number.</param>
    /// <param name="filter">The filter criteria for selecting user roles.</param>
    /// <param name="search">The optional search term to filter user roles.
    ValueTask<IEnumerable<UserRoleViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}