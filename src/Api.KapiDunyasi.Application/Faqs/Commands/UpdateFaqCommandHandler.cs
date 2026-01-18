using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.Faqs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Faqs.Commands;

public class UpdateFaqCommandHandler : IRequestHandler<UpdateFaqCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateFaqCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateFaqCommand request, CancellationToken cancellationToken)
    {
        var faq = await _context.Faqs.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (faq == null)
        {
            throw new InvalidOperationException("SSS bulunamadi.");
        }

        var payload = request.Payload;
        
        typeof(Faq).GetProperty("QTR")!.SetValue(faq, payload.QTR);
        typeof(Faq).GetProperty("ATR")!.SetValue(faq, payload.ATR);
        typeof(Faq).GetProperty("SortOrder")!.SetValue(faq, payload.SortOrder);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
