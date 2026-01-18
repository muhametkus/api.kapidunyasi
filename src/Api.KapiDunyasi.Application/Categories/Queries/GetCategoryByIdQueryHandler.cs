using Api.KapiDunyasi.Application.Categories.Dtos;
using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Categories.Queries;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponseDto?>
{
    private readonly IAppDbContext _context;

    public GetCategoryByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CategoryResponseDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new CategoryResponseDto
            {
                Id = x.Id,
                NameTR = x.NameTR,
                Slug = x.Slug,
                ParentId = x.ParentId,
                DescriptionTR = x.DescriptionTR
            })
            .FirstOrDefaultAsync(cancellationToken);

        return category;
    }
}
