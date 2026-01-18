using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Contacts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Contacts.Queries;

public class GetContactMessagesQueryHandler : IRequestHandler<GetContactMessagesQuery, List<ContactMessageResponseDto>>
{
    private readonly IAppDbContext _context;

    public GetContactMessagesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public Task<List<ContactMessageResponseDto>> Handle(GetContactMessagesQuery request, CancellationToken cancellationToken)
    {
        return _context.ContactMessages
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new ContactMessageResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
