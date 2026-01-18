using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Contacts.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Contacts.Queries;

public class GetContactMessageByIdQueryHandler : IRequestHandler<GetContactMessageByIdQuery, ContactMessageResponseDto?>
{
    private readonly IAppDbContext _context;

    public GetContactMessageByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ContactMessageResponseDto?> Handle(GetContactMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var message = await _context.ContactMessages
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new ContactMessageResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                CreatedAt = x.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        return message;
    }
}
