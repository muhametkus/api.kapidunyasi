using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Orders.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Orders.Queries;

public class GetOrderTrackQueryHandler : IRequestHandler<GetOrderTrackQuery, OrderTrackDto?>
{
    private readonly IAppDbContext _context;

    public GetOrderTrackQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderTrackDto?> Handle(GetOrderTrackQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .AsNoTracking()
            .Where(x => x.OrderNo == request.OrderNo)
            .Select(x => new OrderTrackDto
            {
                OrderNo = x.OrderNo,
                Status = x.Status,
                CreatedAt = x.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        return order;
    }
}
