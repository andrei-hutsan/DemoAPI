using FluentValidation;

namespace DemoAPI.Validators;

public class DepartmentValidator : AbstractValidator<DepartmentModel>
{
    public DepartmentValidator()
    {
            RuleFor(p => p.Name).NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Length(2,50)
            .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");
    }

    protected bool BeAValidName(string name)
    {
        return name.All(Char.IsLetter);
    }
}
