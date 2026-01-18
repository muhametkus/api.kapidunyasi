using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.References.Commands;

public class DeleteReferenceCommandHandler : IRequestHandler<DeleteReferenceCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteReferenceCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteReferenceCommand request, CancellationToken cancellationToken)
    {
        var reference = await _context.References.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reference == null)
        {
            throw new InvalidOperationException("Referans bulunamadi.");
        }

        _context.References.Remove(reference);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
