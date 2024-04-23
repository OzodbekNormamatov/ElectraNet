using AutoMapper;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Transformers;
using ElectraNet.Domain.Enitites.Transformers;

namespace ElectraNet.Service.Services.Transformers;

public class TransformerService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ITransformerService transformerService) : ITransformerService
{
    public async ValueTask<TransformerViewModel> CreateAsync(TransformerCreateModel createModel)
    {
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Description.ToLower() == createModel.Description.ToLower());

        if (existTransformer is not null)
            throw new AlreadyExistException("Transformer is already exist");

        if (createModel.TransformerPointId is not null)
            await transformerService.GetByIdAsync(Convert.ToInt64(createModel.TransformerPointId));

        var transformer = mapper.Map<Transformer>(createModel);
        transformer.Create();
        var createdTransformer = await unitOfWork.Transformers.InsertAsync(transformer);
        await unitOfWork.SaveAsync();

        return mapper.Map<TransformerViewModel>(createModel);
    }

    public async ValueTask<TransformerViewModel> UpdateAsync(long id, TransformerUpdateModel updateModel)
    {
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id && !t.IsDeleted)
            ?? throw new NotFoundException($"Transformer is not found with this ID = {id}");

        if (updateModel.TransformerPointId is not null)
            await transformerService.GetByIdAsync(Convert.ToInt64(updateModel.TransformerPointId));


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
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id && !t.IsDeleted)
           ?? throw new NotFoundException($"Transformer is not found with this ID = {id}");

        existTransformer.Delete();
        await unitOfWork.Transformers.DeleteAsync(existTransformer);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<TransformerViewModel> GetByIdAsync(long id)
    {
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id && !t.IsDeleted)
           ?? throw new NotFoundException($"Transformer is not found with this ID = {id}");

        return mapper.Map<TransformerViewModel>(existTransformer);
    }

    public async ValueTask<IEnumerable<TransformerViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var transformers = unitOfWork.Transformers
          .SelectAsQueryable(expression: e => !e.IsDeleted, includes: ["TransformerPoint"], isTracked: false)
          .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            transformers = transformers.Where(role =>
                role.Description.Contains(search, StringComparison.OrdinalIgnoreCase));

        var paginateTransformer = transformers.ToPaginateAsQueryable(@params).ToListAsync();
        return await Task.FromResult(mapper.Map<IEnumerable<TransformerViewModel>>(transformers));
    }
}
