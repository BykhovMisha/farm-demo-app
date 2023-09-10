using FarmDemoApp.BL.Handlers.Animal;
using FluentValidation;

namespace FarmDemoApp.BL.Validators.Animal;

public class GetAnimalPageQueryValidator : AbstractValidator<GetAnimalPageQuery>
{
    public GetAnimalPageQueryValidator()
    {
        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Skip should be greater than or equal to 0");

        RuleFor(x => x.Take)
            .InclusiveBetween(1, 100)
            .WithMessage("Take should be from 1 to 100");

        RuleFor(x => x.Name)
            .MaximumLength(200)
            .WithMessage("Name maximum length is 200");
    }
}
