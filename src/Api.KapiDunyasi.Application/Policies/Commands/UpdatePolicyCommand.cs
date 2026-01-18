using Api.KapiDunyasi.Application.Policies.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Policies.Commands;

public record UpdatePolicyCommand(string Key, PolicyUpdateRequestDto Payload) : IRequest<Unit>;
