using Api.KapiDunyasi.Application.Policies.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Policies.Queries;

public record GetPoliciesQuery() : IRequest<List<PolicyContentDto>>;
