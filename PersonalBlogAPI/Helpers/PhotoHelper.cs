using Microsoft.AspNetCore.StaticFiles;
using System.Net.Mime;

namespace PersonalBlogAPI.Helpers;

public static class PhotoHelper
{
    private static string _saveToDisk(IFormFile photo)
    {
        string photoName = Guid.NewGuid().ToString();
        string photoExtension = new FileInfo(photo.FileName).Extension;
#if DEBUG
        var filePath = $"bin\\Debug\\net8.0\\Photos\\{photoName + photoExtension}";
#else
        var filePath = $"Photos\\{photoName+photoExtension}";
#endif
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            photo.CopyToAsync(stream);
        }

        return filePath;
    }

    private static bool _validatePhoto(IFormFile photo) // It is better To Validate Image Header Other Than This
    {
        string contentType;
        new FileExtensionContentTypeProvider().TryGetContentType(photo.FileName, out contentType!);
        if (contentType == null) 
        {
            return false;
        }
        return contentType.StartsWith("image/");
    }

    public static string SaveNewPhoto(IFormFile photo)
    {
        if (_validatePhoto(photo))
        {
            return _saveToDisk(photo);
        }
        return null!;
    }

    public static void DeletePhoto(string path)
    {
        File.Delete(path);
    }
}