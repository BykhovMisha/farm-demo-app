using FarmDemoApp.DataAccess.Dto.Animal;
using FarmDemoApp.DataAccess.Dto.Common;

namespace FarmDemoApp.DataAccess.Abstracts.Repository;

public interface IAnimalRepository
{
    public Task<PageDto<AnimalDto>> GetPage(int skip, int take, string? name, CancellationToken cancellationToken = default);

    public Task CreateOrUpdate(AnimalDto animal);

    public Task Delete(int id);
}

