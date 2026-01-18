namespace Api.KapiDunyasi.Domain.Blog;

public class BlogContentSection
{
    public string HeadingTR { get; private set; }
    public string BodyTR { get; private set; }

    protected BlogContentSection() { }

    public BlogContentSection(string headingTR, string bodyTR)
    {
        HeadingTR = headingTR;
        BodyTR = bodyTR;
    }
}