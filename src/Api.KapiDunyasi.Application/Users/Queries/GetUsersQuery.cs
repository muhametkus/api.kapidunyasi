using Api.KapiDunyasi.Application.Users.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Users.Queries;

public record GetUsersQuery() : IRequest<List<UserResponseDto>>;
