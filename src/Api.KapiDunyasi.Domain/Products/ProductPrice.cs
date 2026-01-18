namespace Api.KapiDunyasi.Domain.Products;

public class ProductPrice
{
    public string Type { get; private set; } // fixed | range

    public decimal? Value { get; private set; }
    public decimal? Min { get; private set; }
    public decimal? Max { get; private set; }

    protected ProductPrice() { }

    public static ProductPrice Fixed(decimal value)
        => new("fixed", value, null, null);

    public static ProductPrice Range(decimal min, decimal max)
        => new("range", null, min, max);

    private ProductPrice(string type, decimal? value, decimal? min, decimal? max)
    {
        Type = type;
        Value = value;
        Min = min;
        Max = max;
    }
}