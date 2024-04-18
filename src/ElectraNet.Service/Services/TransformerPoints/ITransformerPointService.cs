using ElectraNet.Domain.Enitites.TransformerPoints;

namespace ElectraNet.Service.Services.TransformerPoints;

public interface ITransformerPointService
{
    ValueTask<TransformerPoint> CreateAsync(TransformerPoint transformerPoint);
    ValueTask<TransformerPoint> UpdateAsync(long id, TransformerPoint transformerPoint);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<TransformerPoint> GetByIdAsync(long id);
    ValueTask<IEnumerable<TransformerPoint>> GetAllAsync(PaganationParams @params, Filter filter, string search = null);
}
