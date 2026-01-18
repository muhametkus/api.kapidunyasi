using Api.KapiDunyasi.Application.Blog.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Blog.Commands;

public record UpdateBlogPostCommand(Guid Id, BlogUpdateRequestDto Payload) : IRequest<Unit>;
