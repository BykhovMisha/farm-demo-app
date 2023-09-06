using FarmDemoApp.DataAccess.Abstracts.Repository;
using FarmDemoApp.DataAccess.Dto.Animal;
using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class CreateOrUpdateAnimalCommandHandler : IRequestHandler<CreateOrUpdateAnimalCommand>
{
    private readonly IAnimalRepository _animalRepository;

    public CreateOrUpdateAnimalCommandHandler(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public async Task Handle(CreateOrUpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        var saveModel = new AnimalDto { Id = request.Id, Name = request.Name };
        await _animalRepository.CreateOrUpdate(saveModel);
    }
}
