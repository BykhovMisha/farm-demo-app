using FarmDemoApp.BL.Handlers.Animal;
using FluentValidation;

namespace FarmDemoApp.BL.Validators.Animal;

public class DeleteAnimalCommandValidator : AbstractValidator<DeleteAnimalCommand>
{
    public DeleteAnimalCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id should be greater than 0");
    }
}
