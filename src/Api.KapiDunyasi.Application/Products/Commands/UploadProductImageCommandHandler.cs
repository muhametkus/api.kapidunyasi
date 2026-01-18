using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Products.Commands;

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand, string>
{
    private readonly IAppDbContext _context;
    private readonly IFileStorageService _fileStorage;

    public UploadProductImageCommandHandler(IAppDbContext context, IFileStorageService fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public async Task<string> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            throw new InvalidOperationException("Ürün bulunamadı.");
        }

        // Dosya uzantısını kontrol et
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
        var extension = Path.GetExtension(request.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension))
        {
            throw new InvalidOperationException("Geçersiz dosya formatı. Sadece jpg, jpeg, png, webp, gif desteklenmektedir.");
        }

        // Dosyayı kaydet
        var relativePath = await _fileStorage.UploadAsync(
            request.FileStream, 
            request.FileName, 
            $"products/{product.Id}", 
            cancellationToken);

        // Ürüne image ekle
        var imageUrl = _fileStorage.GetPublicUrl(relativePath);
        product.AddImage(imageUrl);
        
        await _context.SaveChangesAsync(cancellationToken);

        return imageUrl;
    }
}
