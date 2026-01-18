using Api.KapiDunyasi.Application.Showrooms.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Showrooms.Commands;

public record UpdateShowroomCommand(Guid Id, ShowroomUpdateRequestDto Payload) : IRequest<Unit>;
