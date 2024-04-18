using ElectraNet.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace ElectraNet.Service.DTOs.Assets;

public class AssetCreateModel
{
    public IFormFile File {  get; set; }
    public  FileType FileType { get; set; }
}
