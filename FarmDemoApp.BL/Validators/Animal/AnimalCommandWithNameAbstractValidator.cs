using FarmDemoApp.BL.Handlers.Animal.Abstracts;
using FluentValidation;

namespace FarmDemoApp.BL.Validators.Animal;

public abstract class AnimalCommandWithNameAbstractValidator<T> : AbstractValidator<T> 
    where T : IAnimalCommadWithName
{
    public AnimalCommandWithNameAbstractValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name should not be empty")
            .MaximumLength(200)
            .WithMessage("Name maximum length is 200");
    }
}
