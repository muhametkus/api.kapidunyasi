using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Orders.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Orders.Queries;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderResponseDto>>
{
    private readonly IAppDbContext _context;

    public GetOrdersQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<List<OrderResponseDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return _context.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .OrderByDescending(x => x.CreatedAt)
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
            .ToListAsync(cancellationToken);
    }
}
