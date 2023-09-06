using FarmDemoApp.Common.Exceptions;
using FarmDemoApp.DataAccess.Dto.Animal;
using FarmDemoApp.DataAccess.Entities;
using FarmDemoApp.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FarmDemoApp.DataAccess.Tests.Repostories;

public class AnimalRepositoryTests
{
    [Fact]
    public void GetPage_ReturnsCorrectPage()
    {
        // Arrange
        using (var context = CreateFarmContext())
        {
            var repository = new AnimalRepository(context);

            // Act
            var result = repository.GetPage(0, 1, "O", CancellationToken.None).Result;

            // Assert
            result.Items.Should().HaveCount(1); // There should be 1 item matching the query
            result.TotalCount.Should().Be(2);  // Total count should be 1
            result.Items.Should().OnlyContain(a => a.Name.ToLower().Contains("o"));

        }
    }

    [Fact]
    public void CreateOrUpdate_CreatesNewAnimal()
    {
        // Arrange
        using (var context = CreateFarmContext())
        {
            var repository = new AnimalRepository(context);
            var newAnimal = new AnimalDto { Id = 0, Name = "Horse" };

            // Act
            repository.CreateOrUpdate(newAnimal).Wait();

            // Assert
            context.Animals.Should().HaveCount(4);
            context.Animals.Should().Contain(a => a.Name == "Horse");
        }
    }

    [Fact]
    public async Task CreateOrUpdate_AnimalWithNotUniqueName_FarmAppException()
    {
        // Arrange
        using (var context = CreateFarmContext())
        {
            var repository = new AnimalRepository(context);
            var invalidAnimal = new AnimalDto { Id = 2, Name = "Cow" };

            // Act and Assert
            Func<Task> action = () => repository.CreateOrUpdate(invalidAnimal);
            await action.Should().ThrowAsync<FarmAppException>()
                .WithMessage("Animal name should be unique");
        }
    }

    [Fact]
    public void Delete_RemovesAnimal()
    {
        // Arrange
        using (var context = CreateFarmContext())
        {
            var repository = new AnimalRepository(context);

            // Act
            repository.Delete(1).Wait();

            // Assert
            context.Animals.Should().HaveCount(2); 
            context.Animals.Should().NotContain(a => a.Id == 1); 
        }
    }

    [Fact]
    public async Task Delete_NonExistentAnimalId_ThrowsException()
    {
        // Arrange
        using (var context = CreateFarmContext())
        {
            var repository = new AnimalRepository(context);
            var nonExistentAnimalId = 999;

            // Act and Assert
            Func<Task> action = () => repository.Delete(nonExistentAnimalId);
            await action.Should().ThrowAsync<FarmAppNotFoundException>()
                .WithMessage("Animal not found");
        }
    }

    private static FarmContext CreateFarmContext()
    {
        var options = new DbContextOptionsBuilder<FarmContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FarmContext(options);
        context.Database.EnsureCreated();

        var animals = new[]
        {
            new Animal { Id = 1, Name = "Cow" },
            new Animal { Id = 2, Name = "Goat" },
            new Animal { Id = 3, Name = "Sheep" }
        };

        context.Animals.AddRange(animals);
        context.SaveChanges();

        return context;
    }
}
