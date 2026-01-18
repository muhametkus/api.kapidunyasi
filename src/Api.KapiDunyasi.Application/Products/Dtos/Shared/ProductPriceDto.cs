namespace Api.KapiDunyasi.Application.Products.Dtos.Shared;

public class ProductPriceDto
{
    public string Type { get; set; } = null!; // fixed | range
    public decimal? Value { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
}