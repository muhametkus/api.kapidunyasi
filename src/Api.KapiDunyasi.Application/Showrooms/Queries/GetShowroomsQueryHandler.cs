using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Showrooms.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Showrooms.Queries;

public class GetShowroomsQueryHandler : IRequestHandler<GetShowroomsQuery, List<ShowroomDto>>
{
    private readonly IAppDbContext _context;

    public GetShowroomsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<List<ShowroomDto>> Handle(GetShowroomsQuery request, CancellationToken cancellationToken)
    {
        return _context.Showrooms
            .AsNoTracking()
            .OrderBy(x => x.NameTR)
            .Select(x => new ShowroomDto
            {
                Id = x.Id,
                NameTR = x.NameTR,
                Slug = x.Slug,
                Phone = x.Phone,
                AddressTR = x.AddressTR,
                MapEmbedUrl = x.MapEmbedUrl,
                WorkingHoursTR = x.WorkingHoursTR
            })
            .ToListAsync(cancellationToken);
    }
}
