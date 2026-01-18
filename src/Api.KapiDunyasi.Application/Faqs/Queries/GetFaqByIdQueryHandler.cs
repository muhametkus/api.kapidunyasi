using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Faqs.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Faqs.Queries;

public class GetFaqByIdQueryHandler : IRequestHandler<GetFaqByIdQuery, FaqResponseDto?>
{
    private readonly IAppDbContext _context;

    public GetFaqByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<FaqResponseDto?> Handle(GetFaqByIdQuery request, CancellationToken cancellationToken)
    {
        var faq = await _context.Faqs
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new FaqResponseDto
            {
                Id = x.Id,
                QTR = x.QTR,
                ATR = x.ATR,
                SortOrder = x.SortOrder,
                CreatedAt = x.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        return faq;
    }
}
