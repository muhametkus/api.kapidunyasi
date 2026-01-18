using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Policies.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Policies.Queries;

public class GetPolicyContentQueryHandler : IRequestHandler<GetPolicyContentQuery, PolicyContentDto?>
{
    private readonly IAppDbContext _context;

    public GetPolicyContentQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PolicyContentDto?> Handle(GetPolicyContentQuery request, CancellationToken cancellationToken)
    {
        var policy = await _context.PolicyContents
            .AsNoTracking()
            .Where(x => x.Key == request.Key)
            .Select(x => new PolicyContentDto
            {
                Key = x.Key,
                TitleTR = x.TitleTR,
                BodyTR = x.BodyTR
            })
            .FirstOrDefaultAsync(cancellationToken);

        return policy;
    }
}
