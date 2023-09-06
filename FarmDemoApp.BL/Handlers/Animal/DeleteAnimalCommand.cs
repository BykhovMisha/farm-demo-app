using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class DeleteAnimalCommand : IRequest
{
    public int Id { get; init; }
}