using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Contacts.Commands;

public class DeleteContactMessageCommandHandler : IRequestHandler<DeleteContactMessageCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteContactMessageCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteContactMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _context.ContactMessages.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (message == null)
        {
            throw new InvalidOperationException("Mesaj bulunamadi.");
        }

        _context.ContactMessages.Remove(message);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
