using Api.KapiDunyasi.Application.Blog.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Blog.Queries;

public record GetBlogPostsQuery() : IRequest<List<BlogListDto>>;
