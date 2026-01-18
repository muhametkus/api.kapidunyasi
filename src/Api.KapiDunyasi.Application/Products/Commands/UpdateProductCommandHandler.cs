using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Products.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (product == null)
        {
            throw new InvalidOperationException("Urun bulunamadi.");
        }

        var slugExists = await _context.Products.AnyAsync(
            x => x.Slug == payload.Slug && x.Id != request.Id,
            cancellationToken);
        if (slugExists)
        {
            throw new InvalidOperationException("Bu slug zaten kullaniliyor.");
        }

        typeof(Product).GetProperty("NameTR")!.SetValue(product, payload.NameTR);
        typeof(Product).GetProperty("Slug")!.SetValue(product, payload.Slug);
        typeof(Product).GetProperty("SeriesTR")!.SetValue(product, payload.SeriesTR);
        typeof(Product).GetProperty("CategoryId")!.SetValue(product, payload.CategoryId);
        typeof(Product).GetProperty("StockStatus")!.SetValue(product, payload.StockStatus);
        typeof(Product).GetProperty("StockCount")!.SetValue(product, payload.StockCount);
        typeof(Product).GetProperty("Code")!.SetValue(product, payload.Code);
        typeof(Product).GetProperty("FireResistant")!.SetValue(product, payload.FireResistant);
        typeof(Product).GetProperty("WarrantyTR")!.SetValue(product, payload.WarrantyTR);
        typeof(Product).GetProperty("ShippingTR")!.SetValue(product, payload.ShippingTR);

        var price = payload.Price.Type == "fixed"
            ? ProductPrice.Fixed(payload.Price.Value ?? 0)
            : ProductPrice.Range(payload.Price.Min ?? 0, payload.Price.Max ?? 0);
        typeof(Product).GetProperty("Price")!.SetValue(product, price);

        var specs = new ProductSpecs(payload.Specs.MaterialTR, payload.Specs.SurfaceTR);
        SetSpecsCollection(specs, "_thicknessOptions", payload.Specs.ThicknessOptions);
        SetSpecsCollection(specs, "_frameOptions", payload.Specs.FrameOptions);
        SetSpecsCollection(specs, "_sizes", payload.Specs.Sizes);
        SetSpecsCollection(specs, "_colorOptionsTR", payload.Specs.ColorOptionsTR);
        SetSpecsCollection(specs, "_openingDirectionsTR", payload.Specs.OpeningDirectionsTR);
        typeof(Product).GetProperty("Specs")!.SetValue(product, specs);

        SetCollection(product, "_tagsTR", payload.TagsTR);
        SetCollection(product, "_badgesTR", payload.BadgesTR);
        SetCollection(product, "_images", payload.Images);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
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
