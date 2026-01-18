namespace Api.KapiDunyasi.Application.Orders.Dtos;

public class OrderResponseDto
{
    public Guid Id { get; set; }
    public string OrderNo { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string FormName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string InvoiceType { get; set; } = null!;
    public string Payment { get; set; } = null!;
    public List<OrderItemDto> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
