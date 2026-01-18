namespace Api.KapiDunyasi.Application.Products.Requests;

public class ProductListRequest
{
    public string? Category { get; set; }
    public string? Series { get; set; }
    public string? Search { get; set; }
    public string? Sort { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;

    public bool? FireResistant { get; set; }
    public string? StockStatus { get; set; }
    public string? SurfaceTR { get; set; }
    public string? ColorTR { get; set; }
    public decimal? PriceMin { get; set; }
    public decimal? PriceMax { get; set; }
}
