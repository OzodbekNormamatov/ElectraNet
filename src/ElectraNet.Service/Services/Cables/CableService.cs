using AutoMapper;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Cables;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Cables;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;

namespace ElectraNet.Service.Services.Cables;

public class CableService(IMapper mapper, IUnitOfWork unitOfWork) : ICableService
{
    public async ValueTask<CableViewModel> CableCreateModel(CableCreateModel createModel)
    {
        var existCable = await unitOfWork.Cables.SelectAsync(t => t.Description.ToLower() == createModel.Description.ToLower());

        if (existCable is not null)
            throw new AlreadyExistException("Cable is already exist");
        var cable = mapper.Map<Cable>(createModel);
        cable.Create();
        var createdCable = await unitOfWork.Cables.InsertAsync(cable);
        await unitOfWork.SaveAsync();

        return mapper.Map<CableViewModel>(createModel);
    }

    public async ValueTask<CableViewModel> CableUpdateModel(long Id, CableUpdateModel updateModel)
    {
        var existCable = await unitOfWork.Cables.SelectAsync(p => p.Id == Id && !p.IsDeleted)
            ?? throw new NotFoundException($"Cable is not found with this ID = {Id}");

        var alreadyExistCable = await unitOfWork.Cables.SelectAsync(p => p.Description.ToLower() == updateModel.Description.ToLower());
        if (alreadyExistCable is not null)
            throw new AlreadyExistException("Cable is already exist");

        mapper.Map(updateModel, existCable);
        existCable.Update();
        var updatePosition = await unitOfWork.Cables.UpdateAsync(existCable);
        await unitOfWork.SaveAsync();

        return mapper.Map<CableViewModel>(updatePosition);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existCable = await unitOfWork.Cables.SelectAsync(t => t.Id == id)
           ?? throw new NotFoundException($"Cable is not found with this ID = {id}");

        await unitOfWork.Cables.DropAsync(existCable);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<CableViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var cables = unitOfWork.Cables.SelectAsQueryable().OrderBy(filter).ToPaginate(@params);

        if (!string.IsNullOrEmpty(search))
            cables = cables.Where(role =>
                role.Description.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                role.Voltage.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                role.AssetId.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(mapper.Map<IEnumerable<CableViewModel>>(cables));
    }


    public async ValueTask<CableViewModel> GetByIdAsync(long id)
    {
        var existCable = await unitOfWork.Cables.SelectAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Cable is not found with this ID = {id}");

        return mapper.Map<CableViewModel>(existCable);
    }
}
