using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.References;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.References.Commands;

public class UpdateReferenceCommandHandler : IRequestHandler<UpdateReferenceCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateReferenceCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateReferenceCommand request, CancellationToken cancellationToken)
    {
        var reference = await _context.References.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reference == null)
        {
            throw new InvalidOperationException("Referans bulunamadi.");
        }

        var payload = request.Payload;
        
        typeof(Reference).GetProperty("ProjectNameTR")!.SetValue(reference, payload.ProjectNameTR);
        typeof(Reference).GetProperty("CityTR")!.SetValue(reference, payload.LocationTR);
        typeof(Reference).GetProperty("Year")!.SetValue(reference, payload.Year);
        typeof(Reference).GetProperty("TypeTR")!.SetValue(reference, payload.StatusTR);
        typeof(Reference).GetProperty("DescriptionTR")!.SetValue(reference, payload.DescriptionTR);
        typeof(Reference).GetProperty("ImageUrl")!.SetValue(reference, payload.ImageUrl);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
