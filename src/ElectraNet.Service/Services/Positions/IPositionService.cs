using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Positions;

namespace ElectraNet.Service.Services.Positions;

public interface IPositionService
{
    /// <summary>
    /// Creates a new position based on the provided position creation model.
    /// </summary>
    /// <param name="createModel">The position creation model containing information about the position to create.
    ValueTask<PositionViewModel> CreateAsync(PositionCreateModel createModel);

    /// <summary>
    /// Updates an existing position asynchronously based on the specified ID and the provided position update model.
    /// </summary>
    /// <param name="id">The ID of the position to update.</param>
    /// <param name="updateModel">The position update model containing the new information for the position.
    ValueTask<PositionViewModel> UpdateAsync(long id, PositionUpdateModel updateModel);

    /// <summary>
    /// Deletes a position based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the position to delete.
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves a position based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the position to retrieve.
    ValueTask<PositionViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Retrieves all positions based on the provided pagination parameters, filter, and optional search term.
    /// </summary>
    /// <param name="@params">The pagination parameters specifying the page size and number.</param>
    /// <param name="filter">The filter criteria for selecting positions.</param>
    /// <param name="search">The optional search term to filter positions.
    ValueTask<IEnumerable<PositionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
