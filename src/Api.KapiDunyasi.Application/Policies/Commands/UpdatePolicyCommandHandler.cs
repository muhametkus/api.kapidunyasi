using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Policies.Commands;

public class UpdatePolicyCommandHandler : IRequestHandler<UpdatePolicyCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdatePolicyCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
    {
        var policy = await _context.PolicyContents.FirstOrDefaultAsync(x => x.Key == request.Key, cancellationToken);
        if (policy == null)
        {
            throw new InvalidOperationException("Politika bulunamadi.");
        }

        var payload = request.Payload;
        policy.UpdateContent(payload.TitleTR, payload.BodyTR);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
