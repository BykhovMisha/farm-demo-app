using FarmDemoApp.DataAccess.Abstracts.Repository;
using FarmDemoApp.DataAccess.Dto.Animal;
using FluentValidation;
using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IValidator<CreateAnimalCommand> _validator;

    public CreateAnimalCommandHandler(IAnimalRepository animalRepository, IValidator<CreateAnimalCommand> validator)
    {
        _animalRepository = animalRepository;
        _validator = validator;
    }

    public async Task Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        var saveModel = new AnimalDto { Name = request.Name };
        await _animalRepository.CreateOrUpdate(saveModel);
    }
}
