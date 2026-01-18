using MediatR;

namespace Api.KapiDunyasi.Application.Blog.Commands;

public record DeleteBlogPostCommand(Guid Id) : IRequest<Unit>;
