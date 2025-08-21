using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace DemoAPI.Validators;

public class PersonValidator : AbstractValidator<PersonModel>
{
    public PersonValidator()
    {
        RuleFor(p => p.Firstname).NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Length(2,50)
            .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");
        RuleFor(p => p.Lastname).NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Length(2, 50)
            .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(100).WithMessage("{PropertyName} cannot exceed 100 characters.");
    }

    protected bool BeAValidName(string name)
    {
        return name.All(Char.IsLetter);
    }
}
