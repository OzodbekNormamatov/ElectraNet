using Microsoft.AspNetCore.Mvc;
using ElectraNet.WebApi.Models;
using ElectraNet.Service.DTOs.Assets;
using ElectraNet.Service.Services.Assets;

namespace ElectraNet.WebApi.Controllers;
public class AssetController(IAssetService assetService) : BaseController
{
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(AssetCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await assetService.UploadAsync(createModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await assetService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await assetService.GetByIdAsync(id)
        });
    }
}

