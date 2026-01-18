using Api.KapiDunyasi.Domain.Common;

namespace Api.KapiDunyasi.Domain.References;

public class Reference : AggregateRoot
{
    public string ProjectNameTR { get; private set; }
    public string? CityTR { get; private set; }
    public int? Year { get; private set; }
    public string? TypeTR { get; private set; }
    public string? DescriptionTR { get; private set; }
    public string? ImageUrl { get; private set; }

    protected Reference() { }

    public Reference(string projectNameTR)
    {
        ProjectNameTR = projectNameTR;
    }
}