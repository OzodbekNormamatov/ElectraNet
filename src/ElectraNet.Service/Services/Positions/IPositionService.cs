using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Positions;

namespace ElectraNet.Service.Services.Positions;

public interface IPositionService
{
    ValueTask<PositionViewModel> CreateAsync(PositionCreateModel createModel);
    ValueTask<PositionViewModel> UpdateAsync(long id, PositionUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<PositionViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<PositionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
