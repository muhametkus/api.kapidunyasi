using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.References.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.References.Queries;

public class GetReferencesQueryHandler : IRequestHandler<GetReferencesQuery, List<ReferenceResponseDto>>
{
    private readonly IAppDbContext _context;

    public GetReferencesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<List<ReferenceResponseDto>> Handle(GetReferencesQuery request, CancellationToken cancellationToken)
    {
        return _context.References
            .AsNoTracking()
            .OrderByDescending(x => x.Year)
            .ThenBy(x => x.ProjectNameTR)
            .Select(x => new ReferenceResponseDto
            {
                Id = x.Id,
                ProjectNameTR = x.ProjectNameTR,
                CityTR = x.CityTR,
                Year = x.Year,
                TypeTR = x.TypeTR,
                DescriptionTR = x.DescriptionTR,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
