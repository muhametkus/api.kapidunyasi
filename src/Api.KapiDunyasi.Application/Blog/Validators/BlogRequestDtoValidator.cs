using Api.KapiDunyasi.Application.Blog.Dtos;
using FluentValidation;

namespace Api.KapiDunyasi.Application.Blog.Validators;

public class BlogCreateRequestDtoValidator : AbstractValidator<BlogCreateRequestDto>
{
    public BlogCreateRequestDtoValidator()
    {
        RuleFor(x => x.TitleTR).NotEmpty().Length(2, 200);
        RuleFor(x => x.Slug).NotEmpty().Matches("^[a-z0-9-]+$");
        RuleFor(x => x.ContentTR).NotEmpty();
        RuleForEach(x => x.ContentTR).SetValidator(new BlogContentSectionDtoValidator());
    }
}

public class BlogUpdateRequestDtoValidator : AbstractValidator<BlogUpdateRequestDto>
{
    public BlogUpdateRequestDtoValidator()
    {
        RuleFor(x => x.TitleTR).NotEmpty().Length(2, 200);
        RuleFor(x => x.Slug).NotEmpty().Matches("^[a-z0-9-]+$");
        RuleFor(x => x.ContentTR).NotEmpty();
        RuleForEach(x => x.ContentTR).SetValidator(new BlogContentSectionDtoValidator());
    }
}

public class BlogContentSectionDtoValidator : AbstractValidator<BlogContentSectionDto>
{
    public BlogContentSectionDtoValidator()
    {
        RuleFor(x => x.HeadingTR).NotEmpty().Length(2, 200);
        RuleFor(x => x.BodyTR).NotEmpty().Length(2, 5000);
    }
}
