using MediatR;

namespace Api.KapiDunyasi.Application.Showrooms.Commands;

public record DeleteShowroomCommand(Guid Id) : IRequest<Unit>;
