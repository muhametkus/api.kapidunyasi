using Api.KapiDunyasi.Application.Blog.Dtos;
using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Blog.Queries;

public class GetBlogPostBySlugQueryHandler : IRequestHandler<GetBlogPostBySlugQuery, BlogDetailDto?>
{
    private readonly IAppDbContext _context;

    public GetBlogPostBySlugQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<BlogDetailDto?> Handle(GetBlogPostBySlugQuery request, CancellationToken cancellationToken)
    {
        var blog = await _context.BlogPosts
            .AsNoTracking()
            .Where(x => x.Slug == request.Slug)
            .Select(x => new BlogDetailDto
            {
                Id = x.Id,
                TitleTR = x.TitleTR,
                Slug = x.Slug,
                ExcerptTR = x.ExcerptTR,
                TagsTR = x.TagsTR.ToList(),
                PublishedAt = x.PublishedAt,
                ContentTR = x.ContentTR.Select(c => new BlogContentSectionDto
                {
                    HeadingTR = c.HeadingTR,
                    BodyTR = c.BodyTR
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return blog;
    }
}
