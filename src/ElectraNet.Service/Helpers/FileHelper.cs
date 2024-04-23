using ElectraNet.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace ElectraNet.Service.Helpers;

public static class FileHelper
{
    public static async ValueTask<(string Path, string Name)> CreateFileAsync(IFormFile file, FileType type)
    {
        var directoryPath = Path.Combine(EnvironmentHelper.WebRootPath, nameof(type));
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fullPath = Path.Combine(directoryPath, file.FileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        await fileStream.WriteAsync(bytes);

        return (fullPath, file.FileName);
    }
}
