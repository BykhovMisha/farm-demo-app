using FarmDemoApp.BL.Handlers.Animal;
using FluentValidation;

namespace FarmDemoApp.BL.Validators.Animal;

public class UpdateAnimalCommandValidator : AnimalCommandWithNameAbstractValidator<UpdateAnimalCommand>
{
    public UpdateAnimalCommandValidator() : base()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id should be greater than 0");
    }
}
