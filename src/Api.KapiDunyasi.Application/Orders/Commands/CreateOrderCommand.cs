using Api.KapiDunyasi.Application.Orders.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Orders.Commands;

public record CreateOrderCommand(CreateOrderRequestDto Payload) : IRequest<OrderCreatedDto>;
