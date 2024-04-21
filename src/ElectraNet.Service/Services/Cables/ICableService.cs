using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Cables;

namespace ElectraNet.Service.Services.Cables;

public interface ICableService
{
    ValueTask<CableViewModel> CableCreateModel(CableCreateModel createModel);
    ValueTask<CableViewModel> CableUpdateModel(long Id, CableUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CableViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<CableViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
