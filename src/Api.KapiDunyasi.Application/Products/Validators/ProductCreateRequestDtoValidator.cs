using Api.KapiDunyasi.Application.Products.Dtos;
using FluentValidation;

namespace Api.KapiDunyasi.Application.Products.Validators;

public class ProductCreateRequestDtoValidator : AbstractValidator<ProductCreateRequestDto>
{
    public ProductCreateRequestDtoValidator()
    {
        RuleFor(x => x.NameTR).NotEmpty().Length(2, 200);
        RuleFor(x => x.Slug).NotEmpty().Matches("^[a-z0-9-]+$");
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.StockStatus).NotEmpty().MaximumLength(50);
        RuleFor(x => x.StockCount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Price).SetValidator(new ProductPriceDtoValidator());
        RuleFor(x => x.Specs).NotNull();
    }
}

public class ProductUpdateRequestDtoValidator : AbstractValidator<ProductUpdateRequestDto>
{
    public ProductUpdateRequestDtoValidator()
    {
        RuleFor(x => x.NameTR).NotEmpty().Length(2, 200);
        RuleFor(x => x.Slug).NotEmpty().Matches("^[a-z0-9-]+$");
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.StockStatus).NotEmpty().MaximumLength(50);
        RuleFor(x => x.StockCount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Price).SetValidator(new ProductPriceDtoValidator());
        RuleFor(x => x.Specs).NotNull();
    }
}

public class ProductPriceDtoValidator : AbstractValidator<Api.KapiDunyasi.Application.Products.Dtos.Shared.ProductPriceDto>
{
    public ProductPriceDtoValidator()
    {
        RuleFor(x => x.Type).NotEmpty().Must(t => t == "fixed" || t == "range");
        RuleFor(x => x.Value).NotNull().When(x => x.Type == "fixed");
        RuleFor(x => x.Min).NotNull().When(x => x.Type == "range");
        RuleFor(x => x.Max).NotNull().When(x => x.Type == "range");
        RuleFor(x => x).Must(x => x.Type != "range" || (x.Min <= x.Max))
            .WithMessage("Min, max'tan buyuk olamaz.");
    }
}
