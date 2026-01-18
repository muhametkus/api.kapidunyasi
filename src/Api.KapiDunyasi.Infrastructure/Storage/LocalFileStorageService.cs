using Api.KapiDunyasi.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Api.KapiDunyasi.Infrastructure.Storage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _storagePath;
    private readonly string _baseUrl;

    public LocalFileStorageService(IConfiguration configuration)
    {
        _storagePath = configuration["Storage:LocalPath"] ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        _baseUrl = configuration["Storage:BaseUrl"] ?? "/uploads";
        
        // Klasör yoksa oluştur
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string folder, CancellationToken cancellationToken = default)
    {
        // Folder path oluştur
        var folderPath = Path.Combine(_storagePath, folder);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Unique dosya adı oluştur
        var extension = Path.GetExtension(fileName);
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(folderPath, uniqueFileName);

        // Dosyayı kaydet
        await using var stream = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(stream, cancellationToken);

        // Relative path döndür
        return $"{folder}/{uniqueFileName}";
    }

    public Task DeleteAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(_storagePath, filePath);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        return Task.CompletedTask;
    }

    public string GetPublicUrl(string filePath)
    {
        return $"{_baseUrl}/{filePath}";
    }
}
