using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.References;
using MediatR;

namespace Api.KapiDunyasi.Application.References.Commands;

public class CreateReferenceCommandHandler : IRequestHandler<CreateReferenceCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateReferenceCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateReferenceCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        
        var reference = new Reference(payload.ProjectNameTR);
        
        typeof(Reference).GetProperty("CityTR")!.SetValue(reference, payload.CityTR);
        typeof(Reference).GetProperty("Year")!.SetValue(reference, payload.Year);
        typeof(Reference).GetProperty("TypeTR")!.SetValue(reference, payload.TypeTR);
        typeof(Reference).GetProperty("DescriptionTR")!.SetValue(reference, payload.DescriptionTR);
        
        _context.References.Add(reference);
        await _context.SaveChangesAsync(cancellationToken);

        return reference.Id;
    }
}
