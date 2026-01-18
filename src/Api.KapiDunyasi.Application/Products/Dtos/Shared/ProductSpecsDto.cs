namespace Api.KapiDunyasi.Application.Products.Dtos.Shared;

public class ProductSpecsDto
{
    public string? MaterialTR { get; set; }
    public string? SurfaceTR { get; set; }

    public List<string> ThicknessOptions { get; set; } = new();
    public List<string> FrameOptions { get; set; } = new();
    public List<string> Sizes { get; set; } = new();
    public List<string> ColorOptionsTR { get; set; } = new();
    public List<string> OpeningDirectionsTR { get; set; } = new();
}