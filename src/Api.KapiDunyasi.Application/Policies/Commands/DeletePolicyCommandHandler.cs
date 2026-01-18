using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Policies.Commands;

public class DeletePolicyCommandHandler : IRequestHandler<DeletePolicyCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeletePolicyCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
    {
        var policy = await _context.PolicyContents.FirstOrDefaultAsync(x => x.Key == request.Key, cancellationToken);
        if (policy == null)
        {
            throw new InvalidOperationException("Politika bulunamadi.");
        }

        _context.PolicyContents.Remove(policy);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
