using AutoMapper;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Transformers;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Transformers;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using ElectraNet.Service.Services.TransformerPoints;
using ElectraNet.WebApi.Validator.Transformers;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ElectraNet.Service.Services.Transformers;

public class TransformerService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ITransformerPointService transformerPointService,
    TransformerCreateModelValidator transformerCreateValidator,
    TransformerUpdateModelValidator transformerUpdateValidator) : ITransformerService
{
    public async ValueTask<TransformerViewModel> CreateAsync(TransformerCreateModel createModel)
    {
        var validator = await transformerCreateValidator.ValidateAsync(createModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existTransformerPoint = await transformerPointService.GetByIdAsync(createModel.TransformerPointId);

        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Description.ToLower() == createModel.Description.ToLower() && !t.IsDeleted);

        if (existTransformer is not null)
            throw new AlreadyExistException("Transformer is already exist");

        var transformer = mapper.Map<Transformer>(createModel);
        transformer.Create();
        var createdTransformer = await unitOfWork.Transformers.InsertAsync(transformer);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<TransformerViewModel>(createdTransformer);
        viewModel.TransformerPoint = existTransformerPoint;
        return viewModel;
    }

    public async ValueTask<TransformerViewModel> UpdateAsync(long id, TransformerUpdateModel updateModel)
    {
        var validator = await transformerUpdateValidator.ValidateAsync(updateModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existTransformerPoint = await transformerPointService.GetByIdAsync(updateModel.TransformerPointId);

        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id && !t.IsDeleted)
            ?? throw new NotFoundException($"Transformer is not found with this ID = {id}");

        var alreadyExistTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Description.ToLower() == updateModel.Description.ToLower() && !t.IsDeleted);
        if (alreadyExistTransformer is not null)
            throw new AlreadyExistException("Transformer is already exist");

        mapper.Map(updateModel, existTransformer);
        existTransformer.Update();
        var upateTransformer = await unitOfWork.Transformers.UpdateAsync(existTransformer);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<TransformerViewModel>(upateTransformer);
        viewModel.TransformerPoint = existTransformerPoint;
        return viewModel;
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
        var existTransformer = await unitOfWork.Transformers.SelectAsync(t => t.Id == id && !t.IsDeleted, ["TransformerPoint"])
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
                role.Description.ToLower().Contains(search.ToLower()));

        var paginateTransformer = await transformers.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<TransformerViewModel>>(paginateTransformer);
    }
}
