using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.TransformerPoints;

namespace ElectraNet.Service.Services.Transformers;

public interface ITransformerService
{
    /// <summary>
    /// Creates a new transformer point  based on the provided transformer point creation model.
    /// </summary>
    /// <param name="createModel">The transformer point creation model containing information about the transformer point to create.
    ValueTask<TransformerPointViewModel> CreateAsync(TransformerPointCreateModel createModel);

    /// <summary>
    /// Updates an existing transformer point based on the specified ID and the provided transformer point update model.
    /// </summary>
    /// <param name="id">The ID of the transformer point to update.</param>
    /// <param name="updateModel">The transformer point update model containing the new information for the transformer point.
    ValueTask<TransformerPointViewModel> UpdateAsync(long id, TransformerPointUpdateModel updateModel);

    /// <summary>
    /// Deletes a transformer point based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the transformer point to delete.
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves a transformer point based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the transformer point to retrieve.
    ValueTask<TransformerPointViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Retrieves all transformer points based on the provided pagination parameters, filter, and optional search term.
    /// </summary>
    /// <param name="@params">The pagination parameters specifying the page size and number.</param>
    /// <param name="filter">The filter criteria for selecting transformer points.</param>
    /// <param name="search">The optional search term to filter transformer points.
    ValueTask<IEnumerable<TransformerPointViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
