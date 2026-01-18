using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.Policies;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Policies.Commands;

public class CreatePolicyCommandHandler : IRequestHandler<CreatePolicyCommand, string>
{
    private readonly IAppDbContext _context;

    public CreatePolicyCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        
        var exists = await _context.PolicyContents.AnyAsync(x => x.Key == payload.Key, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Bu key zaten kullaniliyor.");
        }

        var policy = new PolicyContent(payload.Key, payload.TitleTR, payload.BodyTR);
        
        _context.PolicyContents.Add(policy);
        await _context.SaveChangesAsync(cancellationToken);

        return policy.Key;
    }
}
