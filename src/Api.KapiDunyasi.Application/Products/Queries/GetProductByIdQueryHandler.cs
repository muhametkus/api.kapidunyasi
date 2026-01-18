using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Products.Dtos;
using Api.KapiDunyasi.Application.Products.Dtos.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Products.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDetailDto?>
{
    private readonly IAppDbContext _context;

    public GetProductByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDetailDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Where(p => p.Id == request.Id)
            .Select(p => new ProductDetailDto
            {
                Id = p.Id,
                NameTR = p.NameTR,
                Slug = p.Slug,
                SeriesTR = p.SeriesTR,
                CategoryId = p.CategoryId,
                TagsTR = p.TagsTR.ToList(),
                BadgesTR = p.BadgesTR.ToList(),
                Price = new ProductPriceDto
                {
                    Type = p.Price.Type,
                    Value = p.Price.Value,
                    Min = p.Price.Min,
                    Max = p.Price.Max
                },
                StockStatus = p.StockStatus,
                StockCount = p.StockCount,
                Code = p.Code,
                Images = p.Images.ToList(),
                Specs = new ProductSpecsDto
                {
                    MaterialTR = p.Specs.MaterialTR,
                    SurfaceTR = p.Specs.SurfaceTR,
                    ThicknessOptions = p.Specs.ThicknessOptions.ToList(),
                    FrameOptions = p.Specs.FrameOptions.ToList(),
                    Sizes = p.Specs.Sizes.ToList(),
                    ColorOptionsTR = p.Specs.ColorOptionsTR.ToList(),
                    OpeningDirectionsTR = p.Specs.OpeningDirectionsTR.ToList()
                },
                FireResistant = p.FireResistant,
                WarrantyTR = p.WarrantyTR,
                ShippingTR = p.ShippingTR
            })
            .FirstOrDefaultAsync(cancellationToken);

        return product;
    }
}
