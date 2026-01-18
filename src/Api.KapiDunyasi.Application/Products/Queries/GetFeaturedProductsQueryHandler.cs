using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Products.Dtos;
using Api.KapiDunyasi.Application.Products.Dtos.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Products.Queries;

public class GetFeaturedProductsQueryHandler : IRequestHandler<GetFeaturedProductsQuery, List<ProductListDto>>
{
    private static readonly string[] FeaturedBadges = { "Çok Satan", "Yeni", "İndirim" };
    private readonly IAppDbContext _context;

    public GetFeaturedProductsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<List<ProductListDto>> Handle(GetFeaturedProductsQuery request, CancellationToken cancellationToken)
    {
        return _context.Products
            .AsNoTracking()
            .Where(p => p.BadgesTR.Any(b => FeaturedBadges.Contains(b)))
            .OrderByDescending(p => p.CreatedAt)
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
