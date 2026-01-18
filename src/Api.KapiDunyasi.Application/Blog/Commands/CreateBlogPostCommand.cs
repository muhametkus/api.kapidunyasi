using Api.KapiDunyasi.Application.Blog.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Blog.Commands;

public record CreateBlogPostCommand(BlogCreateRequestDto Payload) : IRequest<Guid>;
