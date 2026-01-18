using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Policies.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Policies.Queries;

public class GetPoliciesQueryHandler : IRequestHandler<GetPoliciesQuery, List<PolicyContentDto>>
{
    private readonly IAppDbContext _context;

    public GetPoliciesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<List<PolicyContentDto>> Handle(GetPoliciesQuery request, CancellationToken cancellationToken)
    {
        return _context.PolicyContents
            .AsNoTracking()
            .OrderBy(x => x.Key)
            .Select(x => new PolicyContentDto
            {
                Key = x.Key,
                TitleTR = x.TitleTR,
                BodyTR = x.BodyTR
            })
            .ToListAsync(cancellationToken);
    }
}
