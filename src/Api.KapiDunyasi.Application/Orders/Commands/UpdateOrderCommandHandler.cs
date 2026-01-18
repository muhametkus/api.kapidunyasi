using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Orders.Commands;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateOrderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (order == null)
        {
            throw new InvalidOperationException("Siparis bulunamadi.");
        }

        order.UpdateStatus(request.Payload.Status);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
