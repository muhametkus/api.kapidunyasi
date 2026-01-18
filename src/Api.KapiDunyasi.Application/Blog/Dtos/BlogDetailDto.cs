namespace Api.KapiDunyasi.Application.Blog.Dtos;

public class BlogDetailDto
{
    public Guid Id { get; set; }
    public string TitleTR { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? ExcerptTR { get; set; }
    public List<BlogContentSectionDto> ContentTR { get; set; } = new();
    public List<string> TagsTR { get; set; } = new();
    public DateTime? PublishedAt { get; set; }
}
