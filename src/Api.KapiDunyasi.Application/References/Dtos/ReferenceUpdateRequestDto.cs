namespace Api.KapiDunyasi.Application.References.Dtos;

public class ReferenceUpdateRequestDto
{
    public string ProjectNameTR { get; set; } = null!;
    public string? LocationTR { get; set; }
    public int? Year { get; set; }
    public string? StatusTR { get; set; }
    public string? DescriptionTR { get; set; }
    public string? ImageUrl { get; set; }
}
