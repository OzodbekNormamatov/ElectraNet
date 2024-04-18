using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Service.Exceptions;
using System.Security;

namespace ElectraNet.Service.Services.TransformerPoints;

public class TransformerPointService(IUnitOfWork unitOfWork) : ITransformerPointService
{
    public async ValueTask<TransformerPoint> CreateAsync(TransformerPoint transformerPoint)
    {
        transformerPoint.CreatedByUserId = HttpContextHelper.UserId;
        var createdTransformerPoint = await unitOfWork.TransformerPoints.InsertAsync(transformerPoint);
        await unitOfWork.SaveAsync();
        return createdTransformerPoint;
    }

    public async ValueTask<TransformerPoint> UpdateAsync(long id, TransformerPoint transformerPoint)
    {
        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Id == id)
          ?? throw new NotFoundException($"TransformerPoint is not found with this ID = {id}");

        existTransformerPoint.Title = transformerPoint.Title;
        existTransformerPoint.Address = transformerPoint.Address;
        existTransformerPoint.UpdatedByUserId = transformerPoint.UpdatedByUserId;
        existTransformerPoint.UpdatedAt = DateTime.UtcNow;
        existTransformerPoint.OrganizationId = transformerPoint.OrganizationId;
        existTransformerPoint.Organization = transformerPoint.Organization;

        await unitOfWork.TransformerPoints.UpdateAsync(existTransformerPoint);
        await unitOfWork.SaveAsync();

        return existTransformerPoint;
    }
    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Id == id)
           ?? throw new NotFoundException($"TransformerPoint is not found with this ID = {id}");

        existTransformerPoint.DeletedAt = DateTime.UtcNow;
        await unitOfWork.TransformerPoints.DropAsync(existTransformerPoint);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<TransformerPoint> GetByIdAsync(long id)
    {
        var existTransformerPoint = await unitOfWork.TransformerPoints.SelectAsync(t => t.Id == id)
            ?? throw new NotFoundException($"TransformerPoint is not found with this ID = {id}");

        return existTransformerPoint;
    }

    public async ValueTask<IEnumerable<TransformerPoint>> GetAllAsync(PaganationParams @params, Filter filter, string search = null)
    {
        var transformerPoints = unitOfWork.TransformerPoints.SelectAsQueryable().OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            transformerPoints = transformerPoints.Where(role =>
                role.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                role.Address.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(transformerPoints.ToPaginate(@params));
    }
}
