namespace Api.KapiDunyasi.Application.Showrooms.Dtos;

public class ShowroomUpdateRequestDto
{
    public string NameTR { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Phone { get; set; }
    public string? AddressTR { get; set; }
    public string? CityTR { get; set; }
    public string? Email { get; set; }
    public string? MapEmbedCode { get; set; }
    public string? WorkingHoursTR { get; set; }
    public string? ImageUrl { get; set; }
}
