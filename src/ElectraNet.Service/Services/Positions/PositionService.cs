using AutoMapper;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Service.Configurations;
using ElectraNet.Service.DTOs.Positions;
using ElectraNet.Service.Exceptions;
using ElectraNet.Service.Extensions;

namespace ElectraNet.Service.Services.Positions;

public class PositionService(IMapper mapper, IUnitOfWork unitOfWork) : IPositionService
{
    public async ValueTask<PositionViewModel> CreateAsync(PositionCreateModel createModel)
    {
        var existPosition = await unitOfWork.Positions.SelectAsync(p => p.Name == createModel.Name);

        if (existPosition is not null)
            throw new AlreadyExistException("Position is already exist");

        var position = mapper.Map<Position>(createModel);
        position.Create();
        var createdPosition = await unitOfWork.Positions.InsertAsync(position);
        await unitOfWork.SaveAsync();
        return mapper.Map<PositionViewModel>(createdPosition);
    }

    public async ValueTask<PositionViewModel> UpdateAsync(long id, PositionUpdateModel updateModel)
    {
        var existPosition = await unitOfWork.Positions.SelectAsync(p => p.Id == id && !p.IsDeleted)
            ?? throw new NotFoundException($"Position is not found with this ID = {id}");

        var alreadyExistPosition = await unitOfWork.Positions.SelectAsync(p => p.Name.ToLower() == updateModel.Name.ToLower());
        if (alreadyExistPosition is not null)
            throw new AlreadyExistException("Position is already exist");

        mapper.Map(updateModel, existPosition);
        existPosition.Update();
        var updatePosition = await unitOfWork.Positions.UpdateAsync(existPosition);
        await unitOfWork.SaveAsync();

        return mapper.Map<PositionViewModel>(updatePosition);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existPosition = await unitOfWork.Positions.SelectAsync(p => p.Id == id)
           ?? throw new NotFoundException($"Position is not found with this ID = {id}");

        await unitOfWork.Positions.DropAsync(existPosition);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<PositionViewModel> GetByIdAsync(long id)
    {
        var position = await unitOfWork.Positions.SelectAsync(p => p.Id == id)
           ?? throw new NotFoundException($"Position is not found with this ID = {id}");

        return mapper.Map<PositionViewModel>(position);
    }

    public async ValueTask<IEnumerable<PositionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var positions = unitOfWork.Positions
          .SelectAsQueryable(expression: e => !e.IsDeleted, isTracked: false)
          .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            positions = positions.Where(position =>
                position.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(mapper.Map<IEnumerable<PositionViewModel>>(positions));
    }
}
