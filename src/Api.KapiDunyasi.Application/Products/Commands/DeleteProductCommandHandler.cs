using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Products.Commands;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (product == null)
        {
            throw new InvalidOperationException("Urun bulunamadi.");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
