using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.TransformerPoints;

namespace ElectraNet.Service.Services.TransformerPoints;

public interface ITransformerPointService
{
    ValueTask<TransformerPointViewModel> CreateAsync(TransformerPointCreateModel createModel);
    ValueTask<TransformerPointViewModel> UpdateAsync(long id, TransformerPointUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<TransformerPointViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<TransformerPointViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
