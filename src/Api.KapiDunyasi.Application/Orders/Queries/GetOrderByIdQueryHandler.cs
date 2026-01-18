using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Orders.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Orders.Queries;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponseDto?>
{
    private readonly IAppDbContext _context;

    public GetOrderByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderResponseDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .Where(x => x.Id == request.Id)
            .Select(x => new OrderResponseDto
            {
                Id = x.Id,
                OrderNo = x.OrderNo,
                Status = x.Status,
                FormName = x.FormName,
                Phone = x.Phone,
                Email = x.Email,
                Address = x.Address,
                InvoiceType = x.InvoiceType,
                Payment = x.Payment,
                CreatedAt = x.CreatedAt,
                Items = x.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    NameTR = i.NameTR,
                    Price = i.Price,
                    Image = i.Image,
                    Qty = i.Qty,
                    Size = i.VariantSize,
                    Frame = i.VariantFrame,
                    Direction = i.VariantDirection,
                    Color = i.VariantColor,
                    Thickness = i.VariantThickness
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return order;
    }
}
