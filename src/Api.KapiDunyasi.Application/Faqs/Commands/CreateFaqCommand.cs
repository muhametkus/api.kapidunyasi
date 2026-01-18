using Api.KapiDunyasi.Application.Faqs.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Faqs.Commands;

public record CreateFaqCommand(FaqCreateRequestDto Payload) : IRequest<Guid>;
