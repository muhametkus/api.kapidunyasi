using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Products.Commands;

public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, Unit>
{
    private readonly IAppDbContext _context;
    private readonly IFileStorageService _fileStorage;

    public DeleteProductImageCommandHandler(IAppDbContext context, IFileStorageService fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public async Task<Unit> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            throw new InvalidOperationException("Ürün bulunamadı.");
        }

        var baseUrl = "/uploads/";
        var relativePath = request.ImageUrl.Contains(baseUrl) 
            ? request.ImageUrl.Substring(request.ImageUrl.IndexOf(baseUrl) + baseUrl.Length)
            : request.ImageUrl;

        await _fileStorage.DeleteAsync(relativePath, cancellationToken);
        product.RemoveImage(request.ImageUrl);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
