using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Categories.Commands;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category == null)
        {
            throw new InvalidOperationException("Kategori bulunamadi.");
        }

        var exists = await _context.Categories.AnyAsync(
            x => x.Slug == payload.Slug && x.Id != request.Id,
            cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Bu slug zaten kullaniliyor.");
        }

        // Entity metodları yoksa reflection ile set et (Domain zayıfsa)
        // Eğer Update metodu varsa onu kullanmak daha iyi ama burada reflection örneği vardı.
        // Daha temiz olması için reflection yerine property'leri public setter yapmıştık? Hayır private set.
        // O zaman reflection kullanmaya devam veya entity'e update metodu eklenmeli.
        // Şimdilik reflection kullanmaya devam edelim mevcut kod yapısını bozmamak için.
        
        typeof(Category).GetProperty("NameTR")!.SetValue(category, payload.NameTR);
        typeof(Category).GetProperty("Slug")!.SetValue(category, payload.Slug);
        typeof(Category).GetProperty("DescriptionTR")!.SetValue(category, payload.DescriptionTR);
        typeof(Category).GetProperty("ParentId")!.SetValue(category, payload.ParentId);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
