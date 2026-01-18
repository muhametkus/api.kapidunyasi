using Api.KapiDunyasi.Application.Products.Dtos.Shared;

namespace Api.KapiDunyasi.Application.Products.Dtos;

public class ProductCreateDto
{
    public string NameTR { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public Guid CategoryId { get; set; }

    public ProductPriceDto Price { get; set; } = null!;
    public ProductSpecsDto Specs { get; set; } = null!;

    public string StockStatus { get; set; } = null!;
    public int StockCount { get; set; }

    public bool FireResistant { get; set; }
}
