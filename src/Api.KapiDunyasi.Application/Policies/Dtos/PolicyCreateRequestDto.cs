namespace Api.KapiDunyasi.Application.Policies.Dtos;

public class PolicyCreateRequestDto
{
    public string Key { get; set; } = null!;
    public string TitleTR { get; set; } = null!;
    public string BodyTR { get; set; } = null!;
}
