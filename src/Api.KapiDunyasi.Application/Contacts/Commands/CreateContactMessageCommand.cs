using Api.KapiDunyasi.Application.Contacts.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Contacts.Commands;

public record CreateContactMessageCommand(ContactMessageCreateDto Payload) : IRequest<ContactMessageCreatedDto>;
