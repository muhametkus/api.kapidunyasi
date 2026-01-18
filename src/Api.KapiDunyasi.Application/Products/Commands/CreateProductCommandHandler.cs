using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Products.Commands;
using Api.KapiDunyasi.Application.Products.Dtos;
using Api.KapiDunyasi.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;

        var slugExists = await _context.Products.AnyAsync(x => x.Slug == payload.Slug, cancellationToken);
        if (slugExists)
        {
            throw new InvalidOperationException("Bu slug zaten kullaniliyor.");
        }

        var product = new Product(
            payload.NameTR,
            payload.Slug,
            payload.CategoryId,
            payload.Price.Type == "fixed"
                ? ProductPrice.Fixed(payload.Price.Value ?? 0)
                : ProductPrice.Range(payload.Price.Min ?? 0, payload.Price.Max ?? 0),
            new ProductSpecs(payload.Specs.MaterialTR, payload.Specs.SurfaceTR),
            payload.StockStatus,
            payload.StockCount);

        if (!string.IsNullOrWhiteSpace(payload.SeriesTR))
        {
            typeof(Product).GetProperty("SeriesTR")!.SetValue(product, payload.SeriesTR);
        }

        if (!string.IsNullOrWhiteSpace(payload.Code))
        {
            typeof(Product).GetProperty("Code")!.SetValue(product, payload.Code);
        }

        typeof(Product).GetProperty("FireResistant")!.SetValue(product, payload.FireResistant);
        typeof(Product).GetProperty("WarrantyTR")!.SetValue(product, payload.WarrantyTR);
        typeof(Product).GetProperty("ShippingTR")!.SetValue(product, payload.ShippingTR);

        SetCollection(product, "_tagsTR", payload.TagsTR);
        SetCollection(product, "_badgesTR", payload.BadgesTR);
        SetCollection(product, "_images", payload.Images);

        SetSpecsCollection(product.Specs, "_thicknessOptions", payload.Specs.ThicknessOptions);
        SetSpecsCollection(product.Specs, "_frameOptions", payload.Specs.FrameOptions);
        SetSpecsCollection(product.Specs, "_sizes", payload.Specs.Sizes);
        SetSpecsCollection(product.Specs, "_colorOptionsTR", payload.Specs.ColorOptionsTR);
        SetSpecsCollection(product.Specs, "_openingDirectionsTR", payload.Specs.OpeningDirectionsTR);

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }

    private static void SetCollection(Product product, string fieldName, List<string> values)
    {
        var field = typeof(Product).GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field?.GetValue(product) is List<string> list)
        {
            list.Clear();
            list.AddRange(values);
        }
    }

    private static void SetSpecsCollection(ProductSpecs specs, string fieldName, List<string> values)
    {
        var field = typeof(ProductSpecs).GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field?.GetValue(specs) is List<string> list)
        {
            list.Clear();
            list.AddRange(values);
        }
    }
}
