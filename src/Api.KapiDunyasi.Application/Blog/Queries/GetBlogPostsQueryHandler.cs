using Api.KapiDunyasi.Application.Blog.Dtos;
using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Blog.Queries;

public class GetBlogPostsQueryHandler : IRequestHandler<GetBlogPostsQuery, List<BlogListDto>>
{
    private readonly IAppDbContext _context;

    public GetBlogPostsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<List<BlogListDto>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        return _context.BlogPosts
            .AsNoTracking()
            .OrderByDescending(x => x.PublishedAt)
            .Select(x => new BlogListDto
            {
                Id = x.Id,
                TitleTR = x.TitleTR,
                Slug = x.Slug,
                ExcerptTR = x.ExcerptTR,
                PublishedAt = x.PublishedAt
            })
            .ToListAsync(cancellationToken);
    }
}
