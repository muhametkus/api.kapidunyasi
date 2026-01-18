using Api.KapiDunyasi.Application.Products.Dtos.Shared;

namespace Api.KapiDunyasi.Application.Products.Dtos;

public class ProductUpdateRequestDto
{
    public string NameTR { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? SeriesTR { get; set; }
    public Guid CategoryId { get; set; }
    public List<string> TagsTR { get; set; } = new();
    public List<string> BadgesTR { get; set; } = new();
    public ProductPriceDto Price { get; set; } = null!;
    public string StockStatus { get; set; } = null!;
    public int StockCount { get; set; }
    public string? Code { get; set; }
    public List<string> Images { get; set; } = new();
    public ProductSpecsDto Specs { get; set; } = null!;
    public bool FireResistant { get; set; }
    public string? WarrantyTR { get; set; }
    public string? ShippingTR { get; set; }
}
