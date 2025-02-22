using Application.CQRS.Users.Handlers;
using FluentValidation;

namespace Application.CQRS.Users.Validators;

public class RegisterValidator : AbstractValidator<Register.Command>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("The name can't has more than 100 characters.");
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(100).WithMessage("The surname can't has more than 100 characters.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255).WithMessage("The email can't has more than 255 characters.");
        RuleFor(x => x.Username).NotEmpty().MaximumLength(100).WithMessage("The username can't has more than 100 characters.");
        RuleFor(x => x.FatherName).NotEmpty().MaximumLength(100).WithMessage("The father name can't has more than 100 characters.");
        RuleFor(x => x.Address).NotEmpty().MaximumLength(100).WithMessage("The address can't has more than 255 characters.");
        RuleFor(x => x.MobilePhone).NotEmpty().Must(phone => phone.StartsWith("+994")).WithMessage("Phone number must start with +994.")
            .MaximumLength(20)
            .WithMessage("The phone number can't has more than 20 characters.");
        RuleFor(x => x.CardNumber).NotEmpty()
            .Matches(@"^\d{16}$").WithMessage("The card number must be 16 digits.")
            .MaximumLength(50).WithMessage("The card number can't has more than 50 characters.");
        RuleFor(x => x.TableNumber).NotEmpty().MaximumLength(50).WithMessage("The table number can't has more than 50 characters.");
        RuleFor(x => x.Birthdate).NotEmpty();
        RuleFor(x => x.DateOfEmployment).NotEmpty();
        RuleFor(x => x.DateOfDismissal).NotEmpty();
        RuleFor(x => x.Note).NotEmpty();
    }
}
