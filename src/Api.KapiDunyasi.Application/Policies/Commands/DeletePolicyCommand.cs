using MediatR;

namespace Api.KapiDunyasi.Application.Policies.Commands;

public record DeletePolicyCommand(string Key) : IRequest<Unit>;
