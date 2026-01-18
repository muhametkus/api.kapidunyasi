using Api.KapiDunyasi.Application.Showrooms.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Showrooms.Queries;

public record GetShowroomBySlugQuery(string Slug) : IRequest<ShowroomDto?>;
