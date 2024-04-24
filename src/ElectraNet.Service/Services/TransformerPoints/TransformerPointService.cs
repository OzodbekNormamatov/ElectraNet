using AutoMapper;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.TransformerPoints;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using ElectraNet.Service.Services.Organizations;
using Microsoft.EntityFrameworkCore;

namespace ElectraNet.Service.Services.TransformerPoints;

public class TransformerPointService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IOrganizationService organizationService) : ITransformerPointService
{
    public async ValueTask<TransformerPointViewModel> CreateAsync(TransformerPointCreateModel createModel)
    {
        var existOrganization = await organizationService.GetByIdAsync(createModel.OrganizationId);

        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Title.ToLower() == createModel.Title.ToLower() && !t.IsDeleted);
        if (existTransformerPoint is not null)
            throw new AlreadyExistException("TransformerPoint is already exist");

        var transformer = mapper.Map<TransformerPoint>(createModel);
        transformer.Create();
        var createdTransformerPoint = await unitOfWork.TransformerPoints.InsertAsync(transformer);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<TransformerPointViewModel>(createdTransformerPoint);
        viewModel.Organization = existOrganization;
        return viewModel;
    }

    public async ValueTask<TransformerPointViewModel> UpdateAsync(long id, TransformerPointUpdateModel updateModel)
    {
        var existOrganization = await organizationService.GetByIdAsync(updateModel.OrganizationId);

        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Id == id && !t.IsDeleted)
          ?? throw new NotFoundException($"TransformerPoint is not found with this ID = {id}");

        var alreadyExistTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Title.ToLower() == updateModel.Title.ToLower() && !t.IsDeleted);
        if (alreadyExistTransformerPoint is not null)
            throw new AlreadyExistException("TransformerPoint is already exist");

        mapper.Map(updateModel, existTransformerPoint);
        existTransformerPoint.Update();
        var updateTransformerPoint = await unitOfWork.TransformerPoints.UpdateAsync(existTransformerPoint);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<TransformerPointViewModel>(updateTransformerPoint);
        viewModel.Organization = existOrganization;
        return viewModel;
    }
    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Id == id && !t.IsDeleted)
           ?? throw new NotFoundException($"TransformerPoint is not found with this ID = {id}");

        existTransformerPoint.Delete();
        await unitOfWork.TransformerPoints.DeleteAsync(existTransformerPoint);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<TransformerPointViewModel> GetByIdAsync(long id)
    {
        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Id == id && !t.IsDeleted , ["Organization"])
            ?? throw new NotFoundException($"TransformerPoint is not found with this ID = {id}");

        return mapper.Map<TransformerPointViewModel>(existTransformerPoint);
    }

    public async ValueTask<IEnumerable<TransformerPointViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var transformerPoints = unitOfWork.TransformerPoints
          .SelectAsQueryable(expression: e => !e.IsDeleted, includes: ["Organization"], isTracked: false)
          .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            transformerPoints = transformerPoints.Where(role =>
                role.Title.ToLower().Contains(search.ToLower()) ||
                role.Address.ToLower().Contains(search.ToLower()));

        var paginateTransformerPoint = await transformerPoints.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<TransformerPointViewModel>>(transformerPoints);
    }
}
