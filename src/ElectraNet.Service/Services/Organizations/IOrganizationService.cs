using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Organizations;
using ElectraNet.Service.DTOs.Permissions;

namespace ElectraNet.Service.Services.Organizations;

public interface IOrganizationService
{
    /// <summary>
    /// Deletes an organization based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the organization to delete.
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves an organization based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the organization to retrieve.
    ValueTask<OrganizationViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Creates a new organization based on the provided organization creation model.
    /// </summary>
    /// <param name="createModel">The organization creation model containing information about the organization to create.
    ValueTask<OrganizationViewModel> CreateAsync(OrganizationCreateModel createModel);

    /// <summary>
    /// Updates an existing organization based on the specified ID and the provided organization update model.
    /// </summary>
    /// <param name="id">The ID of the organization to update.</param>
    /// <param name="updateModel">The organization update model containing the new information for the organization.
    ValueTask<OrganizationViewModel> UpdateAsync(long id, OrganizationUpdateModel updateModel);

    /// <summary>
    /// Retrieves all organizations based on the provided pagination parameters, filter, and optional search term.
    /// </summary>
    /// <param name="params">The pagination parameters specifying the page size and number.</param>
    /// <param name="filter">The filter criteria for selecting organizations.</param>
    /// <param name="search">The optional search term to filter organizations.
    ValueTask<IEnumerable<OrganizationViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}