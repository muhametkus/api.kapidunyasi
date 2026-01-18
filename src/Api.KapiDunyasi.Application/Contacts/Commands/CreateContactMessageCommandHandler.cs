using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Application.Contacts.Dtos;
using Api.KapiDunyasi.Domain.Contacts;
using MediatR;

namespace Api.KapiDunyasi.Application.Contacts.Commands;

public class CreateContactMessageCommandHandler
    : IRequestHandler<CreateContactMessageCommand, ContactMessageCreatedDto>
{
    private readonly IAppDbContext _context;

    public CreateContactMessageCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ContactMessageCreatedDto> Handle(CreateContactMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new ContactMessage(request.Payload.Name, request.Payload.Email, request.Payload.Message);

        _context.ContactMessages.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ContactMessageCreatedDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt
        };
    }
}
