using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Service.Exceptions;

namespace ElectraNet.Service.Services.Positions;

public class PositionService(IUnitOfWork unitOfWork) : IPositionService
{
    public async ValueTask<Position> CreateAsync(Position position)
    {
        position.CreatedByUserId = HttpContextHelper.UserId;
        var createdPosition = await unitOfWork.Positions.InsertAsync(position);
        await unitOfWork.SaveAsync();
        return createdPosition;
    }

    public async ValueTask<Position> UpdateAsync(long id, Position position)
    {
        var existPosition = await unitOfWork.Positions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Position is not found with this ID = {id}");

        existPosition.Name = position.Name;
        existPosition.UpdatedAt = DateTime.UtcNow;
        existPosition.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Positions.UpdateAsync(position);
        await unitOfWork.SaveAsync();

        return existPosition;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existPosition = await unitOfWork.Positions.SelectAsync(p => p.Id == id)
           ?? throw new NotFoundException($"Position is not found with this ID = {id}");

        existPosition.DeletedAt = DateTime.UtcNow;
        await unitOfWork.Positions.DropAsync(existPosition);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<Position> GetByIdAsync(long id)
    {
        var existPosition = await unitOfWork.Positions.SelectAsync(p => p.Id == id)
           ?? throw new NotFoundException($"Position is not found with this ID = {id}");

        return existPosition;
    }

    public async ValueTask<IEnumerable<Position>> GetAllAsync(PaganationParams @params, Filter filter, string search = null)
    {
        var positions = unitOfWork.Positions.SelectAsQueryable().OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            positions = positions.Where(role =>
                role.Name.Contains(search, StringComparison.OrdinalIgnoreCase);

        return await Task.FromResult(positions.ToPaginate(@params));
    }
}
