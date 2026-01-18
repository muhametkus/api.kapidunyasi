namespace Api.KapiDunyasi.Application.Faqs.Dtos;

public class FaqResponseDto
{
    public Guid Id { get; set; }
    public string QTR { get; set; } = null!;
    public string ATR { get; set; } = null!;
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; }
}
