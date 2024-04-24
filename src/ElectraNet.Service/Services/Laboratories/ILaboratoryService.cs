using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Laboratories;

namespace ElectraNet.Service.Services.Laboratories;

public interface ILaboratoryService
{
    /// <summary>
    /// Deletes a laboratory based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the laboratory to delete.
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves a laboratory based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the laboratory to retrieve.
    ValueTask<LaboratoryViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Creates a new laboratory asynchronously based on the provided laboratory creation model.
    /// </summary>
    /// <param name="permission">The laboratory creation model containing information about the laboratory to create.
    ValueTask<LaboratoryViewModel> CreateAsync(LaboratoryCreateModel permission);

    /// <summary>
    /// Updates an existing laboratory asynchronously based on the specified ID and the provided laboratory update model.
    /// </summary>
    /// <param name="id">The ID of the laboratory to update.</param>
    /// <param name="permission">The laboratory update model containing the new information for the laboratory.
    ValueTask<LaboratoryViewModel> UpdateAsync(long id, LaboratoryUpdateModel permission);

    /// <summary>
    /// Retrieves all laboratories asynchronously based on the provided pagination parameters, filter, and optional search term.
    /// </summary>
    /// <param name="params">The pagination parameters specifying the page size and number.</param>
    /// <param name="filter">The filter criteria for selecting laboratories.</param>
    /// <param name="search">The optional search term to filter laboratories.</param>
    ValueTask<IEnumerable<LaboratoryViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}