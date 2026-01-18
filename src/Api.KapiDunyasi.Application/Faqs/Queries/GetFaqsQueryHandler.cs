using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Faqs.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Faqs.Queries;

public class GetFaqsQueryHandler : IRequestHandler<GetFaqsQuery, List<FaqDto>>
{
    private readonly IAppDbContext _context;

    public GetFaqsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<List<FaqDto>> Handle(GetFaqsQuery request, CancellationToken cancellationToken)
    {
        return _context.Faqs
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => new FaqDto
            {
                QTR = x.QTR,
                ATR = x.ATR,
                SortOrder = x.SortOrder
            })
            .ToListAsync(cancellationToken);
    }
}
