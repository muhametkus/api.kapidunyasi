using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Categories.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var exists = await _context.Categories.AnyAsync(x => x.Slug == payload.Slug, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Bu slug zaten kullaniliyor.");
        }

        var category = new Category(payload.NameTR, payload.Slug, payload.DescriptionTR, payload.ParentId);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
