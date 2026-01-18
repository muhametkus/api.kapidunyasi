using Api.KapiDunyasi.Application.Policies.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Policies.Commands;

public record CreatePolicyCommand(PolicyCreateRequestDto Payload) : IRequest<string>;
