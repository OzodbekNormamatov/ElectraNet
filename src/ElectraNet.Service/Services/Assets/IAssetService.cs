using ElectraNet.Service.DTOs.Assets;

namespace ElectraNet.Service.Services.Assets;

public interface IAssetService
{
    ValueTask<AssetViewModel> UploadAsync(AssetCreateModel asset);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<AssetViewModel> GetByIdAsync(long id);
}
