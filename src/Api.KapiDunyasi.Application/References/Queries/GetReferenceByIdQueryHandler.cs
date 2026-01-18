using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.References.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.References.Queries;

public class GetReferenceByIdQueryHandler : IRequestHandler<GetReferenceByIdQuery, ReferenceResponseDto?>
{
    private readonly IAppDbContext _context;

    public GetReferenceByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ReferenceResponseDto?> Handle(GetReferenceByIdQuery request, CancellationToken cancellationToken)
    {
        var reference = await _context.References
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        return reference;
    }
}
