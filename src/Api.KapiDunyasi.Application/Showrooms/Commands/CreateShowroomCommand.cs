using Api.KapiDunyasi.Application.Showrooms.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Showrooms.Commands;

public record CreateShowroomCommand(ShowroomCreateRequestDto Payload) : IRequest<Guid>;
