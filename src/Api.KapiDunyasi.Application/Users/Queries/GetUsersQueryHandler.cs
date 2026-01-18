using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Users.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Users.Queries;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserResponseDto>>
{
    private readonly IAppDbContext _context;

    public GetUsersQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<List<UserResponseDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return _context.Users
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new UserResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Role = x.Role,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
