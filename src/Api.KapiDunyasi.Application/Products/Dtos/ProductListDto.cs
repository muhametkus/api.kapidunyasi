using Api.KapiDunyasi.Application.Products.Dtos.Shared;

namespace Api.KapiDunyasi.Application.Products.Dtos;

public class ProductListDto
{
    public Guid Id { get; set; }
    public string NameTR { get; set; } = null!;
    public string Slug { get; set; } = null!;

    public ProductPriceDto Price { get; set; } = null!;
    public string StockStatus { get; set; } = null!;

    public List<string> BadgesTR { get; set; } = new();
    public List<string> Images { get; set; } = new();
}
