using ElectraNet.Domain.Enitites.Transformers;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Transformers;

namespace ElectraNet.Service.Services.Transformers;

public interface ITransformerService
{
    ValueTask<TransformerViewModel> CreateAsync(TransformerCreateModel createModel);
    ValueTask<TransformerViewModel> UpdateAsync(long id, TransformerUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<TransformerViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<TransformerViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
