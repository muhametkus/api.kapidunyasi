using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Orders.Commands;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteOrderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (order == null)
        {
            throw new InvalidOperationException("Siparis bulunamadi.");
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
