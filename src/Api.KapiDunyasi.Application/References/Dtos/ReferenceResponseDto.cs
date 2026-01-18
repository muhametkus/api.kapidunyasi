namespace Api.KapiDunyasi.Application.References.Dtos;

public class ReferenceResponseDto
{
    public Guid Id { get; set; }
    public string ProjectNameTR { get; set; } = null!;
    public string? CityTR { get; set; }
    public int? Year { get; set; }
    public string? TypeTR { get; set; }
    public string? DescriptionTR { get; set; }
    public DateTime CreatedAt { get; set; }
}