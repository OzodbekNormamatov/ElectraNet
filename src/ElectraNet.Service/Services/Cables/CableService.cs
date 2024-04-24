using AutoMapper;
using ElectraNet.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using ElectraNet.Service.Extensions;
using ElectraNet.Service.DTOs.Cables;
using ElectraNet.Service.Configurations;
using ElectraNet.Domain.Enitites.Cables;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Services.Assets;
using ElectraNet.Service.Validators.Cables;

namespace ElectraNet.Service.Services.Cables;

public class CableService(
    IMapper mapper, 
    IUnitOfWork unitOfWork, 
    IAssetService assetService,
    CableCreateModelValidator cableCreateModelValidator,
    CableUpdateModelValidator cableUpdateModelValidator) : ICableService
{
    public async ValueTask<CableViewModel> CreateAsync(CableCreateModel createModel)
    {
        var validator = await cableCreateModelValidator.ValidateAsync(createModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existAsset = await assetService.GetByIdAsync(createModel.AssetId);

        var cable = mapper.Map<Cable>(createModel);
        cable.Create();
        var createdCable = await unitOfWork.Cables.InsertAsync(cable);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<CableViewModel>(createdCable);
        viewModel.Asset = existAsset;
        return viewModel;
    }

    public async ValueTask<CableViewModel> UpdateAsync(long Id, CableUpdateModel updateModel)
    {
        var validator = await cableUpdateModelValidator.ValidateAsync(updateModel);
        if (!validator.IsValid)
            throw new ArgumentIsNotValidException(validator.Errors.FirstOrDefault().ErrorMessage);

        var existAsset = await assetService.GetByIdAsync(updateModel.AssetId);

        var existCable = await unitOfWork.Cables.SelectAsync(cable => cable.Id == Id && !cable.IsDeleted)
            ?? throw new NotFoundException($"Cable is not found with this ID = {Id}");

        mapper.Map(existCable, updateModel);
        existCable.Update();
        var updateCable = await unitOfWork.Cables.UpdateAsync(existCable);
        await unitOfWork.SaveAsync();

        var viewModel = mapper.Map<CableViewModel>(updateCable);
        viewModel.Asset = existAsset;
        return viewModel;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existCable = await unitOfWork.Cables.SelectAsync(cable => cable.Id == id && !cable.IsDeleted)
           ?? throw new NotFoundException($"Cable is not found with this ID = {id}");

        existCable.Delete();
        await unitOfWork.Cables.DeleteAsync(existCable);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<CableViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var cables = unitOfWork.Cables.
            SelectAsQueryable(expression: cable => !cable.IsDeleted, includes: ["Asset"], isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            cables = cables.Where(role =>
                role.Description.ToLower().Contains(search.ToLower()));

        var paginateCables = await cables.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<CableViewModel>>(paginateCables);
    }

    public async ValueTask<CableViewModel> GetByIdAsync(long id)
    {
        var existCable = await unitOfWork.Cables.SelectAsync(cable => cable.Id == id  && !cable.IsDeleted, ["Asset"])
            ?? throw new NotFoundException($"Cable is not found with this ID = {id}");

        return mapper.Map<CableViewModel>(existCable);
    }
}
