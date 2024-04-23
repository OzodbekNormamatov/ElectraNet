using ElectraNet.Domain.Enums;
using Microsoft.AspNetCore.Http;
using ElectraNet.Domain.Enitites.Commons;
using ElectraNet.Service.DTOs.Assets;
using ElectraNet.Service.DTOs.UserRoles;
using ElectraNet.Service.Configurations;

namespace ElectraNet.Service.Services.Assets;

public interface IAssetService
{
    ValueTask<AssetViewModel> UploadAsync(AssetCreateModel asset);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<AssetViewModel> GetByIdAsync(long id);
    Task<object> CreateAsync(AssetCreateModel createModel);
    Task<object> UpdateAsync(long id, AssetUpdateModel updateModel);
    Task<object> GetAllAsync(PaginationParams @params, Filter filter);
}
