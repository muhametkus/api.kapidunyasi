namespace Api.KapiDunyasi.Application.Showrooms.Dtos;

public class ShowroomDto
{
    public Guid Id { get; set; }
    public string NameTR { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Phone { get; set; }
    public string? AddressTR { get; set; }
    public string? MapEmbedUrl { get; set; }
    public string? WorkingHoursTR { get; set; }
}
