namespace Api.KapiDunyasi.Application.Orders.Dtos;

public class CreateOrderRequestDto
{
    public List<OrderItemDto> Items { get; set; } = new();
    public OrderFormDto Form { get; set; } = null!;
}
