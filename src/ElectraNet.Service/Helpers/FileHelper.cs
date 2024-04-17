﻿using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Domain.Enitites.Commons;
using ElectraNet.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace ElectraNet.Service.Helpers;

public static class FileHelper
{
    public static async ValueTask<(string Path, string Name)> CreateFileAsync(IUnitOfWork unitOfWork, IFormFile file, FileType type)
    {
        var directoryPath = Path.Combine(PathHelper.WebRootPath, nameof(type));
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fullPath = Path.Combine(directoryPath, file.FileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        await fileStream.WriteAsync(bytes);
        var asset = new Asset
        {
            Path = fullPath,
            Name = file.Name,
            CreatedByUserId = HttpContextHelper.UserId
        };

        await unitOfWork.Assets.InsertAsync(asset);
        await unitOfWork.SaveAsync();
        return (fullPath, file.FileName);
    }
}