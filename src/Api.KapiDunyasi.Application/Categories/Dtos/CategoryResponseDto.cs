namespace Api.KapiDunyasi.Application.Categories.Dtos;

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string NameTR { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public Guid? ParentId { get; set; }
    public string? DescriptionTR { get; set; }
}
