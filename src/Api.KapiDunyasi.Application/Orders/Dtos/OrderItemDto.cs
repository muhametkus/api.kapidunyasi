namespace Api.KapiDunyasi.Application.Orders.Dtos;

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string NameTR { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public int Qty { get; set; }

    public string? Size { get; set; }
    public string? Frame { get; set; }
    public string? Direction { get; set; }
    public string? Color { get; set; }
    public string? Thickness { get; set; }
}
