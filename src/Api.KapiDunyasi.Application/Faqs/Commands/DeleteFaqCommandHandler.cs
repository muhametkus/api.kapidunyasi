using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Faqs.Commands;

public class DeleteFaqCommandHandler : IRequestHandler<DeleteFaqCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteFaqCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteFaqCommand request, CancellationToken cancellationToken)
    {
        var faq = await _context.Faqs.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (faq == null)
        {
            throw new InvalidOperationException("SSS bulunamadi.");
        }

        _context.Faqs.Remove(faq);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
