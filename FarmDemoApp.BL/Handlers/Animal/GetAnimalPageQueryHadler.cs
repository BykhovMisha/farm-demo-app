using FarmDemoApp.BL.Models.Common;
using FarmDemoApp.DataAccess.Abstracts.Repository;
using FarmDemoApp.DataAccess.Dto.Animal;
using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class GetAnimalPageQueryHadler : IRequestHandler<GetAnimalPageQuery, PageModel<AnimalModel>>
{
    private readonly IAnimalRepository _animalRepository;

    public GetAnimalPageQueryHadler(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public async Task<PageModel<AnimalModel>> Handle(GetAnimalPageQuery request, CancellationToken cancellationToken)
    {
        var page = await _animalRepository.GetPage(request.Skip, request.Take, request.Name, cancellationToken);
        return new PageModel<AnimalModel>
        {
            TotalCount = page.TotalCount,
            Items = page.Items
                .Select(MapDto)
                .ToList()
        };
    }

    private static AnimalModel MapDto(AnimalDto dto)
    {
        return new AnimalModel
        {
            Id = dto.Id,
            Name = dto.Name,
        };
    }
}