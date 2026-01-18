using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Showrooms.Commands;

public class DeleteShowroomCommandHandler : IRequestHandler<DeleteShowroomCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteShowroomCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteShowroomCommand request, CancellationToken cancellationToken)
    {
        var showroom = await _context.Showrooms.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (showroom == null)
        {
            throw new InvalidOperationException("Showroom bulunamadi.");
        }

        _context.Showrooms.Remove(showroom);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
