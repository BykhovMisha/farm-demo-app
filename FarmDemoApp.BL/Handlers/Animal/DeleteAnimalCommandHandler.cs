using FarmDemoApp.DataAccess.Abstracts.Repository;
using FluentValidation;
using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class DeleteAnimalCommandHandler : IRequestHandler<DeleteAnimalCommand>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IValidator<DeleteAnimalCommand> _validator;

    public DeleteAnimalCommandHandler(IAnimalRepository animalRepository, IValidator<DeleteAnimalCommand> validator)
    {
        _animalRepository = animalRepository;
        _validator = validator;
    }

    public async Task Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        await _animalRepository.Delete(request.Id);
    }
}
