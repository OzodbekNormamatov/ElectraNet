using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Cables;

namespace ElectraNet.Service.Services.Cables;

public interface ICableService
{
    /// <summary>
    /// Creates a new cable asynchronously based on the provided cable creation model.
    /// </summary>
    /// <param name="createModel"></param>
    /// <returns></returns>
    ValueTask<CableViewModel> CreateAsync(CableCreateModel createModel);

    /// <summary>
    ///  model.
    /// </summary>
    /// <param name="Id">The ID of the cable to update.
    ValueTask<CableViewModel> UpdateAsync(long Id, CableUpdateModel updateModel);

    /// <summary>
    /// Deletes a cable based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the cable to delete.
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves a cable based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the cable to retrieve.
    ValueTask<CableViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Retrieves all cables based on the provided pagination parameters, filter, and optional search term.
    /// </summary>
    /// <param name="@params">The pagination parameters specifying the page size and number.</param>
    /// <param name="filter">The filter criteria for selecting cables.</param>
    /// <param name="search">The optional search term to filter cables.</param>
    ValueTask<IEnumerable<CableViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
