using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Blog.Commands;

public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteBlogPostCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (post == null)
        {
            throw new InvalidOperationException("Blog bulunamadi.");
        }

        _context.BlogPosts.Remove(post);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
