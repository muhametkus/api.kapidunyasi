using Api.KapiDunyasi.Application.Products.Dtos.Shared;

namespace Api.KapiDunyasi.Application.Products.Dtos;

public class ProductUpdateDto
{
    public string? NameTR { get; set; }
    public string? SeriesTR { get; set; }
    public ProductPriceDto? Price { get; set; }

    public string? StockStatus { get; set; }
    public int? StockCount { get; set; }

    public bool? FireResistant { get; set; }
}
