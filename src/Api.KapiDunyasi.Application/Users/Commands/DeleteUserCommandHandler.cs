using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Users.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (user == null)
        {
            throw new InvalidOperationException("Kullanici bulunamadi.");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
