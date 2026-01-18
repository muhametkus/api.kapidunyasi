using Api.KapiDunyasi.Application.Orders.Dtos;
using FluentValidation;

namespace Api.KapiDunyasi.Application.Orders.Validators;

public class CreateOrderRequestDtoValidator : AbstractValidator<CreateOrderRequestDto>
{
    public CreateOrderRequestDtoValidator()
    {
        RuleFor(x => x.Items).NotEmpty();
        RuleForEach(x => x.Items).SetValidator(new OrderItemDtoValidator());
        RuleFor(x => x.Form).SetValidator(new OrderFormDtoValidator());
    }
}

public class OrderFormDtoValidator : AbstractValidator<OrderFormDto>
{
    public OrderFormDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 100);
        RuleFor(x => x.Phone).NotEmpty().Length(8, 20);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.Address).NotEmpty().Length(10, 500);
        RuleFor(x => x.InvoiceType).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Payment).NotEmpty().MaximumLength(50);
    }
}

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.NameTR).NotEmpty().Length(2, 200);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Qty).GreaterThan(0);
    }
}
