using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Laboratories;

namespace ElectraNet.Service.Services.Laboratories;

public interface ILaboratoryService
{
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<LaboratoryViewModel> GetByIdAsync(long id);
    ValueTask<LaboratoryViewModel> CreateAsync(LaboratoryCreateModel permission);
    ValueTask<LaboratoryViewModel> UpdateAsync(long id, LaboratoryUpdateModel permission);
    ValueTask<IEnumerable<LaboratoryViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}