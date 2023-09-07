using FarmDemoApp.BL.Handlers.Animal.Abstracts;
using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class CreateAnimalCommand : IRequest, IAnimalCommadWithName
{
    public required string Name { get; init; }
}
