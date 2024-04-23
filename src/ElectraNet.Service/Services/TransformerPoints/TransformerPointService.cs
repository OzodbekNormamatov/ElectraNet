using AutoMapper;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.TransformerPoints;
using ElectraNet.Service.Services.Organizations;
using ElectraNet.Domain.Enitites.TransformerPoints;

namespace ElectraNet.Service.Services.TransformerPoints;

public class TransformerPointService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IOrganizationService organizationService) : ITransformerPointService
{
    public async ValueTask<TransformerPointViewModel> CreateAsync(TransformerPointCreateModel createModel)
    {
        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Title.ToLower() == createModel.Title.ToLower());

        if (existTransformerPoint is not null)
            throw new AlreadyExistException("TransformerPoint is already exist");

        if (createModel.OrganizationId is not null)
            await organizationService.GetByIdAsync(Convert.ToInt64(createModel.OrganizationId));

        var transformer = mapper.Map<TransformerPoint>(createModel);
        transformer.Create();
        var createdTransformerPoint = await unitOfWork.TransformerPoints.InsertAsync(transformer);
        await unitOfWork.SaveAsync();

        return mapper.Map<TransformerPointViewModel>(createModel);
    }

    public async ValueTask<TransformerPointViewModel> UpdateAsync(long id, TransformerPointUpdateModel updateModel)
    {
        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Id == id && !t.IsDeleted)
          ?? throw new NotFoundException($"TransformerPoint is not found with this ID = {id}");

        if (updateModel.OrganizationId is not null)
            await organizationService.GetByIdAsync(Convert.ToInt64(updateModel.OrganizationId));

        var alreadyExistTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Title.ToLower() == updateModel.Title.ToLower());
        if (alreadyExistTransformerPoint is not null)
            throw new AlreadyExistException("TransformerPoint is already exist");

        mapper.Map(existTransformerPoint, updateModel);
        existTransformerPoint.Update();
        var updateTransformerPoint = await unitOfWork.TransformerPoints.UpdateAsync(existTransformerPoint);
        await unitOfWork.SaveAsync();

        return mapper.Map<TransformerPointViewModel>(updateTransformerPoint);
    }
    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Id == id)
           ?? throw new NotFoundException($"TransformerPoint is not found with this ID = {id}");

        existTransformerPoint.Delete();
        await unitOfWork.TransformerPoints.DeleteAsync(existTransformerPoint);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<TransformerPointViewModel> GetByIdAsync(long id)
    {
        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Id == id)
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
                role.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                role.Address.Contains(search, StringComparison.OrdinalIgnoreCase));

        var paginateTransformerpoint = transformerPoints.ToPaginateAsQueryable(@params).ToListAsync();
        return await Task.FromResult(mapper.Map<IEnumerable<TransformerPointViewModel>>(transformerPoints));
    }
}
