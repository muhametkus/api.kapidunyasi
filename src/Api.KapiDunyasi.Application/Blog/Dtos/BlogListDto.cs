namespace Api.KapiDunyasi.Application.Blog.Dtos;

public class BlogListDto
{
    public Guid Id { get; set; }
    public string TitleTR { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? ExcerptTR { get; set; }
    public DateTime? PublishedAt { get; set; }
}
