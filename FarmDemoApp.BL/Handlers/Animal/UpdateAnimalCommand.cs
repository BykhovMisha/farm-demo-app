using FarmDemoApp.BL.Handlers.Animal.Abstracts;
using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class UpdateAnimalCommand : IRequest, IAnimalCommadWithName
{
    public int Id { get; init; }

    public required string Name { get; init; }
}
