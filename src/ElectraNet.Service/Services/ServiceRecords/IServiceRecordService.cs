using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.ServiceRecords;

namespace ElectraNet.Service.Services.ServiceRecords;

public interface IServiceRecordService
{
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<ServiceRecordViewModel> GetByIdAsync(long id);
    ValueTask<ServiceRecordViewModel> CreateAsync(ServiceRecordCreateModel createModel);
    ValueTask<ServiceRecordViewModel> UpdateAsync(long id, ServiceRecordUpdateModel updateModel);
    ValueTask<IEnumerable<ServiceRecordViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}