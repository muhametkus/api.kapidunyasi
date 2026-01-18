using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.Showrooms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Showrooms.Commands;

public class CreateShowroomCommandHandler : IRequestHandler<CreateShowroomCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateShowroomCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateShowroomCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        
        var exists = await _context.Showrooms.AnyAsync(x => x.Slug == payload.Slug, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Bu slug zaten kullaniliyor.");
        }

        var showroom = new Showroom(payload.NameTR, payload.Slug);
        
        typeof(Showroom).GetProperty("Phone")!.SetValue(showroom, payload.Phone);
        typeof(Showroom).GetProperty("AddressTR")!.SetValue(showroom, payload.AddressTR);
        typeof(Showroom).GetProperty("MapEmbedUrl")!.SetValue(showroom, payload.MapEmbedUrl);
        typeof(Showroom).GetProperty("WorkingHoursTR")!.SetValue(showroom, payload.WorkingHoursTR);
        
        _context.Showrooms.Add(showroom);
        await _context.SaveChangesAsync(cancellationToken);

        return showroom.Id;
    }
}
