using Api.KapiDunyasi.Application.Blog.Dtos;
using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.Blog;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Blog.Commands;

public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateBlogPostCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var post = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (post == null)
        {
            throw new InvalidOperationException("Blog bulunamadi.");
        }

        var slugExists = await _context.BlogPosts.AnyAsync(
            x => x.Slug == payload.Slug && x.Id != request.Id,
            cancellationToken);
        if (slugExists)
        {
            throw new InvalidOperationException("Bu slug zaten kullaniliyor.");
        }

        typeof(BlogPost).GetProperty("TitleTR")!.SetValue(post, payload.TitleTR);
        typeof(BlogPost).GetProperty("Slug")!.SetValue(post, payload.Slug);
        typeof(BlogPost).GetProperty("ExcerptTR")!.SetValue(post, payload.ExcerptTR);
        typeof(BlogPost).GetProperty("PublishedAt")!.SetValue(post, payload.PublishedAt);

        SetCollection(post, "_tagsTR", payload.TagsTR);
        SetSections(post, payload.ContentTR);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }

    private static void SetCollection(BlogPost post, string fieldName, List<string> values)
    {
        var field = typeof(BlogPost).GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field?.GetValue(post) is List<string> list)
        {
            list.Clear();
            list.AddRange(values);
        }
    }

    private static void SetSections(BlogPost post, List<BlogContentSectionDto> sections)
    {
        var field = typeof(BlogPost).GetField("_contentTR", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field?.GetValue(post) is List<BlogContentSection> list)
        {
            list.Clear();
            list.AddRange(sections.Select(s => new BlogContentSection(s.HeadingTR, s.BodyTR)));
        }
    }
}
