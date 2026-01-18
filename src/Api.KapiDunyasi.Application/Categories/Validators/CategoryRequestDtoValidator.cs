using Api.KapiDunyasi.Application.Categories.Dtos;
using FluentValidation;

namespace Api.KapiDunyasi.Application.Categories.Validators;

public class CategoryCreateRequestDtoValidator : AbstractValidator<CategoryCreateRequestDto>
{
    public CategoryCreateRequestDtoValidator()
    {
        RuleFor(x => x.NameTR).NotEmpty().Length(2, 200);
        RuleFor(x => x.Slug).NotEmpty().Matches("^[a-z0-9-]+$");
    }
}

public class CategoryUpdateRequestDtoValidator : AbstractValidator<CategoryUpdateRequestDto>
{
    public CategoryUpdateRequestDtoValidator()
    {
        RuleFor(x => x.NameTR).NotEmpty().Length(2, 200);
        RuleFor(x => x.Slug).NotEmpty().Matches("^[a-z0-9-]+$");
    }
}
