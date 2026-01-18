using MediatR;

namespace Api.KapiDunyasi.Application.Faqs.Commands;

public record DeleteFaqCommand(Guid Id) : IRequest<Unit>;
