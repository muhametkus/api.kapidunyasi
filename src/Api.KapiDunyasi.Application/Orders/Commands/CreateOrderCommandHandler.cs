using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Orders.Dtos;
using Api.KapiDunyasi.Domain.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderCreatedDto>
{
    private readonly IAppDbContext _context;

    public CreateOrderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderCreatedDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderNo = await GenerateOrderNoAsync(cancellationToken);
        var form = request.Payload.Form;

        var order = new Order(
            orderNo,
            form.Name,
            form.Phone,
            form.Email,
            form.Address,
            form.InvoiceType,
            form.Payment);

        foreach (var item in request.Payload.Items)
        {
            var orderItem = new OrderItem(
                item.ProductId,
                item.NameTR,
                item.Price,
                item.Qty,
                item.Image,
                item.Size,
                item.Frame,
                item.Direction,
                item.Color,
                item.Thickness);

            order.AddItem(orderItem);
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new OrderCreatedDto
        {
            OrderNo = order.OrderNo,
            Status = order.Status
        };
    }

    private async Task<string> GenerateOrderNoAsync(CancellationToken cancellationToken)
    {
        for (var i = 0; i < 5; i++)
        {
            var candidate = $"KD-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";
            var exists = await _context.Orders.AnyAsync(o => o.OrderNo == candidate, cancellationToken);
            if (!exists)
            {
                return candidate;
            }
        }

        return $"KD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}";
    }
}
