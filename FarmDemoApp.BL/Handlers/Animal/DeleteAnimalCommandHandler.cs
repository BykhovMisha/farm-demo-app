using FarmDemoApp.DataAccess.Abstracts.Repository;
using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class DeleteAnimalCommandHandler : IRequestHandler<DeleteAnimalCommand>
{
    private readonly IAnimalRepository _animalRepository;

    public DeleteAnimalCommandHandler(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public async Task Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
    {
        await _animalRepository.Delete(request.Id);
    }
}
