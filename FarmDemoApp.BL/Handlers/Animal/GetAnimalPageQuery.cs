using FarmDemoApp.BL.Models.Common;
using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class GetAnimalPageQuery : IRequest<PageModel<AnimalModel>>
{
    public int Take { get; init; }

    public int Skip { get; init; }

    public string? Name { get; init; }
}