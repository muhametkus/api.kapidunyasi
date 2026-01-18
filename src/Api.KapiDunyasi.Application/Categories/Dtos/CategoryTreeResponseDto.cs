namespace Api.KapiDunyasi.Application.Categories.Dtos;

public class CategoryTreeResponseDto
{
    public Guid Id { get; set; }
    public string NameTR { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? DescriptionTR { get; set; }
    public List<CategoryTreeResponseDto> Children { get; set; } = new();
}
