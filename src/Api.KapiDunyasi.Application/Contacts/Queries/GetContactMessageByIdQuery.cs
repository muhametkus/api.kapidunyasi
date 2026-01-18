using Api.KapiDunyasi.Application.Contacts.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Contacts.Queries;

public record GetContactMessageByIdQuery(Guid Id) : IRequest<ContactMessageResponseDto?>;
