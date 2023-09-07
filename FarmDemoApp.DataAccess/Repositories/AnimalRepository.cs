using FarmDemoApp.Common.Exceptions;
using FarmDemoApp.DataAccess.Abstracts.Repository;
using FarmDemoApp.DataAccess.Dto.Animal;
using FarmDemoApp.DataAccess.Dto.Common;
using FarmDemoApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmDemoApp.DataAccess.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly FarmContext _context;

    public AnimalRepository(FarmContext context)
    {
        _context = context;
    }

    public async Task<PageDto<AnimalDto>> GetPage(int skip, int take, string? name, CancellationToken cancellationToken = default)
    {
        _context.SaveChanges();
        var query = _context.Animals.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
        }

        var count = query.Count();
        var items = await query
            .Skip(skip)
            .Take(take)
            .Select(x => new AnimalDto
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync(cancellationToken);

        return new PageDto<AnimalDto>
        {
            TotalCount = count,
            Items = items
        };
    }

    public async Task CreateOrUpdate(AnimalDto animal)
    {
        if (await _context.Animals.AnyAsync(x => x.Id != animal.Id && x.Name == animal.Name))
        {
            throw new FarmAppException("Animal name should be unique");
        }

        Animal? entity;

        if (animal.Id == 0)
        {
            entity = new Animal();
            _context.Animals.Add(entity);
        }
        else
        {
            entity = await _context.Animals.SingleOrDefaultAsync(x => x.Id == animal.Id);
        }

        if (entity == null)
        {
            throw new FarmAppNotFoundException("Animal not found");
        }

        entity.Name = animal.Name;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var animal = await _context.Animals.SingleOrDefaultAsync(x => x.Id == id);

        if (animal == null)
        {
            throw new FarmAppNotFoundException("Animal not found");
        }

        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();
    }
}
