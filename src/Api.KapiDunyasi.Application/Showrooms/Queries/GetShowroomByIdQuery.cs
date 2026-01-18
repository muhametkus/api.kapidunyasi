using Api.KapiDunyasi.Application.Showrooms.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Showrooms.Queries;

public record GetShowroomByIdQuery(Guid Id) : IRequest<ShowroomDto?>;
