using ElectraNet.Domain.Enitites.Positions;

namespace ElectraNet.Service.Services.Positions;

public interface IPositionService
{
    ValueTask<Position> CreateAsync(Position position);
    ValueTask<Position> UpdateAsync(long id, Position position);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Position> GetByIdAsync(long id);
    ValueTask<IEnumerable<Position>> GetAllAsync(PaganationParams @params, Filter filter, string search = null);
}
