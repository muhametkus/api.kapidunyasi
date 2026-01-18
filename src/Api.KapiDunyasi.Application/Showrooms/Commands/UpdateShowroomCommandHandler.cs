using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.Showrooms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Showrooms.Commands;

public class UpdateShowroomCommandHandler : IRequestHandler<UpdateShowroomCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateShowroomCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateShowroomCommand request, CancellationToken cancellationToken)
    {
        var showroom = await _context.Showrooms.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (showroom == null)
        {
            throw new InvalidOperationException("Showroom bulunamadi.");
        }

        var payload = request.Payload;
        
        // Slug check
        var exists = await _context.Showrooms.AnyAsync(
            x => x.Slug == payload.Slug && x.Id != request.Id,
            cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Bu slug zaten kullaniliyor.");
        }
        
        typeof(Showroom).GetProperty("NameTR")!.SetValue(showroom, payload.NameTR);
        typeof(Showroom).GetProperty("Slug")!.SetValue(showroom, payload.Slug);
        typeof(Showroom).GetProperty("AddressTR")!.SetValue(showroom, payload.AddressTR);
        typeof(Showroom).GetProperty("CityTR")!.SetValue(showroom, payload.CityTR);
        typeof(Showroom).GetProperty("Phone")!.SetValue(showroom, payload.Phone);
        typeof(Showroom).GetProperty("Email")!.SetValue(showroom, payload.Email);
        typeof(Showroom).GetProperty("WorkingHoursTR")!.SetValue(showroom, payload.WorkingHoursTR);
        typeof(Showroom).GetProperty("MapEmbedUrl")!.SetValue(showroom, payload.MapEmbedCode);
        typeof(Showroom).GetProperty("ImageUrl")!.SetValue(showroom, payload.ImageUrl);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
