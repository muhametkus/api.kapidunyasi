using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Showrooms.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Showrooms.Queries;

public class GetShowroomByIdQueryHandler : IRequestHandler<GetShowroomByIdQuery, ShowroomDto?>
{
    private readonly IAppDbContext _context;

    public GetShowroomByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ShowroomDto?> Handle(GetShowroomByIdQuery request, CancellationToken cancellationToken)
    {
        var showroom = await _context.Showrooms
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
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
