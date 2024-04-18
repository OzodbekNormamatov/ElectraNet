using ElectraNet.Domain.Enitites.Transformers;

namespace ElectraNet.Service.Services.Transformers;

public interface ITransformerService
{
    ValueTask<Transformer> CreateAsync(Transformer transformer);
    ValueTask<Transformer> UpdateAsync(long id, Transformer transformer);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Transformer> GetByIdAsync(long id);
    ValueTask<IEnumerable<Transformer>> GetAllAsync(PaganationParams @params, Filter filter, string search = null);
}
