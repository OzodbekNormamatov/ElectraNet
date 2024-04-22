using AutoMapper;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Transformers;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Transformers;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;

namespace ElectraNet.Service.Services.Transformers;

public class TransformerService(IMapper mapper, IUnitOfWork unitOfWork) : ITransformerService
{
    public async ValueTask<TransformerViewModel> CreateAsync(TransformerCreateModel createModel)
    {
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Description.ToLower() == createModel.Description.ToLower());

        if (existTransformer is not null)
            throw new AlreadyExistException("Transformer is already exist");

        var transformer = mapper.Map<Transformer>(createModel);
        transformer.Create();
        var createdTransformer = await unitOfWork.Transformers.InsertAsync(transformer);
        await unitOfWork.SaveAsync();

        return mapper.Map<TransformerViewModel>(createModel);
    }

    public async ValueTask<TransformerViewModel> UpdateAsync(long id, TransformerUpdateModel updateModel)
    {
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Transformer is not found with this ID = {id}");

        var alreadyExistTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Description.ToLower() == updateModel.Description.ToLower());
        if (alreadyExistTransformer is not null)
            throw new AlreadyExistException("Transformer is already exist");

        mapper.Map(existTransformer, updateModel);
        existTransformer.Update();
        var upateTransformer = await unitOfWork.Transformers.UpdateAsync(existTransformer);
        await unitOfWork.SaveAsync();

        return mapper.Map<TransformerViewModel>(upateTransformer);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id)
           ?? throw new NotFoundException($"Transformer is not found with this ID = {id}");

        await unitOfWork.Transformers.DropAsync(existTransformer);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<TransformerViewModel> GetByIdAsync(long id)
    {
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id)
           ?? throw new NotFoundException($"Transformer is not found with this ID = {id}");

        return mapper.Map<TransformerViewModel>(existTransformer);
    }

    public async ValueTask<IEnumerable<TransformerViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var transformers = unitOfWork.Transformers.SelectAsQueryable().OrderBy(filter).ToPaginateAsQueryable(@params);

        if (!string.IsNullOrEmpty(search))
            transformers = transformers.Where(role =>
                role.Description.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(mapper.Map<IEnumerable<TransformerViewModel>>(transformers));
    }
}
