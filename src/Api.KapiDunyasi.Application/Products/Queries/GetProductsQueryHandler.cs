using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Common.Models;
using Api.KapiDunyasi.Application.Products.Dtos;
using Api.KapiDunyasi.Application.Products.Dtos.Shared;
using Api.KapiDunyasi.Application.Products.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductListDto>>
{
    private readonly IAppDbContext _context;

    public GetProductsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<ProductListDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsNoTracking().AsQueryable();
        var filters = request.Request;

        if (!string.IsNullOrWhiteSpace(filters.Category))
        {
            query = query.Where(p => _context.Categories
                .Any(c => c.Id == p.CategoryId && c.Slug == filters.Category));
        }

        if (!string.IsNullOrWhiteSpace(filters.Series))
        {
            query = query.Where(p => p.SeriesTR == filters.Series);
        }

        if (!string.IsNullOrWhiteSpace(filters.Search))
        {
            var search = filters.Search.Trim().ToLowerInvariant();
            query = query.Where(p => p.NameTR.ToLower().Contains(search)
                || p.TagsTR.Any(t => t.ToLower().Contains(search))
                || (p.SeriesTR != null && p.SeriesTR.ToLower().Contains(search)));
        }

        if (filters.FireResistant.HasValue)
        {
            query = query.Where(p => p.FireResistant == filters.FireResistant.Value);
        }

        if (!string.IsNullOrWhiteSpace(filters.StockStatus))
        {
            query = query.Where(p => p.StockStatus == filters.StockStatus);
        }

        if (!string.IsNullOrWhiteSpace(filters.SurfaceTR))
        {
            query = query.Where(p => p.Specs.SurfaceTR == filters.SurfaceTR);
        }

        if (!string.IsNullOrWhiteSpace(filters.ColorTR))
        {
            query = query.Where(p => p.Specs.ColorOptionsTR.Contains(filters.ColorTR));
        }

        if (filters.PriceMin.HasValue || filters.PriceMax.HasValue)
        {
            var min = filters.PriceMin ?? 0m;
            var max = filters.PriceMax ?? decimal.MaxValue;

            query = query.Where(p =>
                (p.Price.Type == "fixed" && p.Price.Value >= min && p.Price.Value <= max)
                || (p.Price.Type == "range" && p.Price.Min <= max && p.Price.Max >= min));
        }

        query = filters.Sort switch
        {
            "fiyat-artan" => query.OrderBy(p => p.Price.Value).ThenBy(p => p.Price.Min),
            "fiyat-azalan" => query.OrderByDescending(p => p.Price.Value).ThenByDescending(p => p.Price.Max),
            "yeniler" => query.OrderByDescending(p => p.CreatedAt),
            "cok-satan" => query.OrderByDescending(p => p.BadgesTR.Contains("Çok Satan"))
                .ThenByDescending(p => p.CreatedAt),
            _ => query.OrderByDescending(p => p.CreatedAt)
        };

        var total = await query.CountAsync(cancellationToken);
        var page = filters.Page <= 0 ? 1 : filters.Page;
        var pageSize = filters.PageSize <= 0 ? 12 : filters.PageSize;

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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

        return new PagedResult<ProductListDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            Total = total
        };
    }
}
