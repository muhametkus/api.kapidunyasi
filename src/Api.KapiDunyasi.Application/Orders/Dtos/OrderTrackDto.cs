namespace Api.KapiDunyasi.Application.Orders.Dtos;

public class OrderTrackDto
{
    public string OrderNo { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
