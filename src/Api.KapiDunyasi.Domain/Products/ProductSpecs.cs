namespace Api.KapiDunyasi.Domain.Products;

public class ProductSpecs
{
    public string? MaterialTR { get; private set; }
    public string? SurfaceTR { get; private set; }

    public IReadOnlyCollection<string> ThicknessOptions => _thicknessOptions;
    private readonly List<string> _thicknessOptions = new();

    public IReadOnlyCollection<string> FrameOptions => _frameOptions;
    private readonly List<string> _frameOptions = new();

    public IReadOnlyCollection<string> Sizes => _sizes;
    private readonly List<string> _sizes = new();

    public IReadOnlyCollection<string> ColorOptionsTR => _colorOptionsTR;
    private readonly List<string> _colorOptionsTR = new();

    public IReadOnlyCollection<string> OpeningDirectionsTR => _openingDirectionsTR;
    private readonly List<string> _openingDirectionsTR = new();

    protected ProductSpecs() { }

    public ProductSpecs(
        string? materialTR,
        string? surfaceTR)
    {
        MaterialTR = materialTR;
        SurfaceTR = surfaceTR;
    }
}