using Api.KapiDunyasi.Domain.Common;

namespace Api.KapiDunyasi.Domain.Products;

public class Product : AggregateRoot
{
    public string NameTR { get; private set; }
    public string Slug { get; private set; }
    public string? SeriesTR { get; private set; }

    public Guid CategoryId { get; private set; }

    public IReadOnlyCollection<string> TagsTR => _tagsTR;
    private readonly List<string> _tagsTR = new();

    public IReadOnlyCollection<string> BadgesTR => _badgesTR;
    private readonly List<string> _badgesTR = new();

    public ProductPrice Price { get; private set; }

    public string StockStatus { get; private set; }
    public int StockCount { get; private set; }

    public string? Code { get; private set; }

    public IReadOnlyCollection<string> Images => _images;
    private readonly List<string> _images = new();

    public ProductSpecs Specs { get; private set; }

    public bool FireResistant { get; private set; }
    public string? WarrantyTR { get; private set; }
    public string? ShippingTR { get; private set; }

    protected Product() { }

    public Product(
        string nameTR,
        string slug,
        Guid categoryId,
        ProductPrice price,
        ProductSpecs specs,
        string stockStatus,
        int stockCount)
    {
        NameTR = nameTR;
        Slug = slug;
        CategoryId = categoryId;
        Price = price;
        Specs = specs;
        StockStatus = stockStatus;
        StockCount = stockCount;
    }

    public void AddImage(string imageUrl)
    {
        if (!string.IsNullOrWhiteSpace(imageUrl) && !_images.Contains(imageUrl))
        {
            _images.Add(imageUrl);
            SetUpdated();
        }
    }

    public void RemoveImage(string imageUrl)
    {
        if (_images.Remove(imageUrl))
        {
            SetUpdated();
        }
    }

    public void ClearImages()
    {
        _images.Clear();
        SetUpdated();
    }

    public void SetImages(IEnumerable<string> images)
    {
        _images.Clear();
        _images.AddRange(images.Where(x => !string.IsNullOrWhiteSpace(x)));
        SetUpdated();
    }
}