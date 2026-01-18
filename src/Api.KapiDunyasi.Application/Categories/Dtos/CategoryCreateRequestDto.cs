namespace Api.KapiDunyasi.Application.Categories.Dtos;

public class CategoryCreateRequestDto
{
    public string NameTR { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? DescriptionTR { get; set; }
    public Guid? ParentId { get; set; }
}
