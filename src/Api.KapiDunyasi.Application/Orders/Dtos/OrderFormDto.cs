namespace Api.KapiDunyasi.Application.Orders.Dtos;

public class OrderFormDto
{
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string InvoiceType { get; set; } = null!;
    public string Payment { get; set; } = null!;
}
