using Api.KapiDunyasi.Application.Orders.Dtos;
using MediatR;

namespace Api.KapiDunyasi.Application.Orders.Queries;

public record GetOrderTrackQuery(string OrderNo) : IRequest<OrderTrackDto?>;
