using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class CreateOrUpdateAnimalCommand : IRequest
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;
}
