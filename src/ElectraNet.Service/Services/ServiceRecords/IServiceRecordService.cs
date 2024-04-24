using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.ServiceRecords;

namespace ElectraNet.Service.Services.ServiceRecords;

public interface IServiceRecordService
{
    /// <summary>
    /// Deletes a service record based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the service record to delete.
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves a service record based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the service record to retrieve.
    ValueTask<ServiceRecordViewModel> GetByIdAsync(long id);

    /// <summary>
    /// Creates a new service record based on the provided service record creation model.
    /// </summary>
    /// <param name="createModel">The service record creation model containing information about the service record to create.
    ValueTask<ServiceRecordViewModel> CreateAsync(ServiceRecordCreateModel createModel);

    /// <summary>
    /// Updates an existing service record based on the specified ID and the provided service record update model.
    /// </summary>
    /// <param name="id">The ID of the service record to update.</param>
    /// <param name="updateModel">The service record update model containing the new information for the service record.
    ValueTask<ServiceRecordViewModel> UpdateAsync(long id, ServiceRecordUpdateModel updateModel);

    /// <summary>
    /// Retrieves all service record based on the provided pagination parameters, filter, and optional search term.
    /// </summary>
    /// <param name="@params">The pagination parameters specifying the page size and number.</param>
    /// <param name="filter">The filter criteria for selecting service records.</param>
    /// <param name="search">The optional search term to filter service records.
    ValueTask<IEnumerable<ServiceRecordViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}