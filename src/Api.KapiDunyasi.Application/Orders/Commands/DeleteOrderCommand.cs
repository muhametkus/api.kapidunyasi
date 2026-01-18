using MediatR;

namespace Api.KapiDunyasi.Application.Orders.Commands;

public record DeleteOrderCommand(Guid Id) : IRequest<Unit>;
