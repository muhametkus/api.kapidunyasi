using Api.KapiDunyasi.Application.Categories.Dtos;
using Api.KapiDunyasi.Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Categories.Queries;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryResponseDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<CategoryResponseDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return _context.Categories
            .AsNoTracking()
            .OrderBy(x => x.NameTR)
            .ProjectTo<CategoryResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
