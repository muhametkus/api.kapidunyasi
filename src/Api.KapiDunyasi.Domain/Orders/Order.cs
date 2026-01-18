using Api.KapiDunyasi.Domain.Common;

namespace Api.KapiDunyasi.Domain.Orders;

public class Order : AggregateRoot
{
    public string OrderNo { get; private set; }
    public string Status { get; private set; }

    public string FormName { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public string InvoiceType { get; private set; }
    public string Payment { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;
    private readonly List<OrderItem> _items = new();

    protected Order() { }

    public Order(
        string orderNo,
        string formName,
        string phone,
        string email,
        string address,
        string invoiceType,
        string payment)
    {
        OrderNo = orderNo;
        FormName = formName;
        Phone = phone;
        Email = email;
        Address = address;
        InvoiceType = invoiceType;
        Payment = payment;
        Status = "pending";
    }

    public void AddItem(OrderItem item)
    {
        _items.Add(item);
    }

    public void UpdateStatus(string status)
    {
        Status = status;
        SetUpdated();
    }
}
