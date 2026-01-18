namespace Api.KapiDunyasi.Application.Faqs.Dtos;

public class FaqCreateRequestDto
{
    public string QTR { get; set; } = null!;
    public string ATR { get; set; } = null!;
    public int SortOrder { get; set; }
}
