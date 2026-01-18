using Api.KapiDunyasi.Application.Orders.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Orders.Commands;

public record UpdateOrderCommand(Guid Id, OrderUpdateRequestDto Payload) : IRequest<Unit>;
