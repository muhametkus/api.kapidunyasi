using Api.KapiDunyasi.Application.Faqs.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Faqs.Commands;

public record UpdateFaqCommand(Guid Id, FaqUpdateRequestDto Payload) : IRequest<Unit>;
