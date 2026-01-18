using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Showrooms.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Showrooms.Queries;

public class GetShowroomBySlugQueryHandler : IRequestHandler<GetShowroomBySlugQuery, ShowroomDto?>
{
    private readonly IAppDbContext _context;

    public GetShowroomBySlugQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ShowroomDto?> Handle(GetShowroomBySlugQuery request, CancellationToken cancellationToken)
    {
        var showroom = await _context.Showrooms
            .AsNoTracking()
            .Where(x => x.Slug == request.Slug)
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
            .FirstOrDefaultAsync(cancellationToken);

        return showroom;
    }
}
