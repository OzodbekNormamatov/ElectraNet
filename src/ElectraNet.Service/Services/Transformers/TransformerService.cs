using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Transformers;
using ElectraNet.Service.Exceptions;

namespace ElectraNet.Service.Services.Transformers;

public class TransformerService(IUnitOfWork unitOfWork) : ITransformerService
{
    public async ValueTask<Transformer> CreateAsync(Transformer transformer)
    {
        transformer.CreatedByUserId = HttpContextHelper.UserId;
        var createdTransformer = await unitOfWork.Transformers.InsertAsync(transformer);
        await unitOfWork.SaveAsync();
        return createdTransformer;
    }

    public async ValueTask<Transformer> UpdateAsync(long id, Transformer transformer)
    {
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Transformer is not found with this ID = {id}");

        existTransformer.Description = transformer.Description;
        existTransformer.TransformerPointId = transformer.TransformerPointId;
        existTransformer.UpdatedByUserId = HttpContextHelper.UserId;
        existTransformer.UpdatedAt = DateTime.UtcNow;
        existTransformer.TransformerPoint = transformer.TransformerPoint;
        await unitOfWork.Transformers.UpdateAsync(transformer);
        await unitOfWork.SaveAsync();

        return existTransformer;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id)
           ?? throw new NotFoundException($"Transformer is not found with this ID = {id}");

        existTransformer.DeletedAt = DateTime.UtcNow;
        await unitOfWork.Transformers.DropAsync(existTransformer);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<Transformer> GetByIdAsync(long id)
    {
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id)
           ?? throw new NotFoundException($"Transformer is not found with this ID = {id}");

        return existTransformer;
    }

    public async ValueTask<IEnumerable<Transformer>> GetAllAsync(PaganationParams @params, Filter filter, string search = null)
    {
        var transformers = unitOfWork.Transformers.SelectAsQueryable().OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            transformers = transformers.Where(role =>
                role.Description.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(transformers.ToPaginate(@params));
    }
}
