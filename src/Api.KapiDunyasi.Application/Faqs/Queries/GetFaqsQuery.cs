using Api.KapiDunyasi.Application.Faqs.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Faqs.Queries;

public record GetFaqsQuery() : IRequest<List<FaqDto>>;
