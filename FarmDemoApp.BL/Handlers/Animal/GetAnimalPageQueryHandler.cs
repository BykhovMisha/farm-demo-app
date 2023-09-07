using FarmDemoApp.BL.Models.Common;
using FarmDemoApp.DataAccess.Abstracts.Repository;
using FarmDemoApp.DataAccess.Dto.Animal;
using FluentValidation;
using MediatR;

namespace FarmDemoApp.BL.Handlers.Animal;

public class GetAnimalPageQueryHandler : IRequestHandler<GetAnimalPageQuery, PageModel<AnimalModel>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IValidator<GetAnimalPageQuery> _validator;

    public GetAnimalPageQueryHandler(IAnimalRepository animalRepository, IValidator<GetAnimalPageQuery> validator)
    {
        _animalRepository = animalRepository;
        _validator = validator;
    }

    public async Task<PageModel<AnimalModel>> Handle(GetAnimalPageQuery request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

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