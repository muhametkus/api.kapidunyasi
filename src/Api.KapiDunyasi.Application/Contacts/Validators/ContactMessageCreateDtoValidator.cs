using Api.KapiDunyasi.Application.Contacts.Dtos;
using FluentValidation;

namespace Api.KapiDunyasi.Application.Contacts.Validators;

public class ContactMessageCreateDtoValidator : AbstractValidator<ContactMessageCreateDto>
{
    public ContactMessageCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.Message).NotEmpty().Length(5, 1000);
    }
}
