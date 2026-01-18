using Api.KapiDunyasi.Domain.Common;

namespace Api.KapiDunyasi.Domain.Orders;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; private set; }
    public string NameTR { get; private set; }
    public decimal Price { get; private set; }
    public string? Image { get; private set; }
    public int Qty { get; private set; }

    public string? VariantSize { get; private set; }
    public string? VariantFrame { get; private set; }
    public string? VariantDirection { get; private set; }
    public string? VariantColor { get; private set; }
    public string? VariantThickness { get; private set; }

    protected OrderItem() { }

    public OrderItem(Guid productId, string nameTR, decimal price, int qty)
    {
        ProductId = productId;
        NameTR = nameTR;
        Price = price;
        Qty = qty;
    }

    public OrderItem(
        Guid productId,
        string nameTR,
        decimal price,
        int qty,
        string? image,
        string? variantSize,
        string? variantFrame,
        string? variantDirection,
        string? variantColor,
        string? variantThickness)
    {
        ProductId = productId;
        NameTR = nameTR;
        Price = price;
        Qty = qty;
        Image = image;
        VariantSize = variantSize;
        VariantFrame = variantFrame;
        VariantDirection = variantDirection;
        VariantColor = variantColor;
        VariantThickness = variantThickness;
    }
}
