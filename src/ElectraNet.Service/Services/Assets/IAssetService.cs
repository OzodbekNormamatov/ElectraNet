using ElectraNet.Service.DTOs.Assets;

namespace ElectraNet.Service.Services.Assets;

public interface IAssetService
{
    /// <summary>
    /// Uploads an asset asynchronously using the provided asset creation model.
    /// </summary>
    /// <param name="asset">The asset creation model containing information about the asset to upload.
    ValueTask<AssetViewModel> UploadAsync(AssetCreateModel asset);

    /// <summary>
    /// Deletes an asset asynchronously based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the asset to delete.</param>
    ValueTask<bool> DeleteAsync(long id);

    /// <summary>
    /// Retrieves an asset asynchronously based on the specified ID.
    /// </summary>
    /// <param name="id">The ID of the asset to retrieve.
    ValueTask<AssetViewModel> GetByIdAsync(long id);

}
