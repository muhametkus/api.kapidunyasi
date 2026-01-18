using Api.KapiDunyasi.Domain.Common;

namespace Api.KapiDunyasi.Domain.Blog;

public class BlogPost : AggregateRoot
{
    public string TitleTR { get; private set; }
    public string Slug { get; private set; }
    public string? ExcerptTR { get; private set; }

    public IReadOnlyCollection<BlogContentSection> ContentTR => _contentTR;
    private readonly List<BlogContentSection> _contentTR = new();

    public IReadOnlyCollection<string> TagsTR => _tagsTR;
    private readonly List<string> _tagsTR = new();

    public DateTime? PublishedAt { get; private set; }

    protected BlogPost() { }

    public BlogPost(string titleTR, string slug)
    {
        TitleTR = titleTR;
        Slug = slug;
    }
}