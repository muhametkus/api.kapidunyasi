using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Categories.Commands;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category == null)
        {
            throw new InvalidOperationException("Kategori bulunamadi.");
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
