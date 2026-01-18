using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.Faqs;
using MediatR;

namespace Api.KapiDunyasi.Application.Faqs.Commands;

public class CreateFaqCommandHandler : IRequestHandler<CreateFaqCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateFaqCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateFaqCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        
        var faq = new Faq(payload.QTR, payload.ATR, payload.SortOrder);
        
        _context.Faqs.Add(faq);
        await _context.SaveChangesAsync(cancellationToken);

        return faq.Id;
    }
}
