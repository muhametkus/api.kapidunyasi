using Api.KapiDunyasi.Domain.Common;

namespace Api.KapiDunyasi.Domain.Showrooms;

public class Showroom : AggregateRoot
{
    public string NameTR { get; private set; }
    public string Slug { get; private set; }
    public string? Phone { get; private set; }
    public string? AddressTR { get; private set; }
    public string? MapEmbedUrl { get; private set; }
    public string? WorkingHoursTR { get; private set; }
    public string? CityTR { get; private set; }
    public string? Email { get; private set; }
    public string? ImageUrl { get; private set; }

    protected Showroom() { }

    public Showroom(string nameTR, string slug)
    {
        NameTR = nameTR;
        Slug = slug;
    }
}