using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Products.Dtos;
using Api.KapiDunyasi.Application.Products.Dtos.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Products.Queries;

public class GetRelatedProductsQueryHandler : IRequestHandler<GetRelatedProductsQuery, List<ProductListDto>>
{
    private readonly IAppDbContext _context;

    public GetRelatedProductsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<List<ProductListDto>> Handle(GetRelatedProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsNoTracking().AsQueryable();

        if (request.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == request.CategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Series))
        {
            query = query.Where(p => p.SeriesTR == request.Series);
        }

        if (request.ExcludeId.HasValue)
        {
            query = query.Where(p => p.Id != request.ExcludeId.Value);
        }

        return query
            .OrderByDescending(p => p.CreatedAt)
            .Take(request.Take)
            .Select(p => new ProductListDto
            {
                Id = p.Id,
                NameTR = p.NameTR,
                Slug = p.Slug,
                Price = new ProductPriceDto
                {
                    Type = p.Price.Type,
                    Value = p.Price.Value,
                    Min = p.Price.Min,
                    Max = p.Price.Max
                },
                StockStatus = p.StockStatus,
                BadgesTR = p.BadgesTR.ToList(),
                Images = p.Images.ToList()
            })
            .ToListAsync(cancellationToken);
    }
}
