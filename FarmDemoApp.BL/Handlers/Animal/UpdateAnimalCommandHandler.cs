using FarmDemoApp.DataAccess.Abstracts.Repository;
using FarmDemoApp.DataAccess.Dto.Animal;
using FluentValidation;
using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IValidator<UpdateAnimalCommand> _validator;

    public UpdateAnimalCommandHandler(IAnimalRepository animalRepository, IValidator<UpdateAnimalCommand> validator)
    {
        _animalRepository = animalRepository;
        _validator = validator;
    }

    public async Task Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        var saveModel = new AnimalDto { Id = request.Id, Name = request.Name };
        await _animalRepository.CreateOrUpdate(saveModel);
    }
}
