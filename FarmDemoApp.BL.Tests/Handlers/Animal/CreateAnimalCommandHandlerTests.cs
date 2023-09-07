using AutoFixture;
using FarmDemoApp.BL.Handlers.Animal;
using FarmDemoApp.DataAccess.Abstracts.Repository;
using FarmDemoApp.DataAccess.Dto.Animal;
using FluentValidation;
using NSubstitute;

namespace FarmDemoApp.BL.Tests.Handlers.Animal;

public class CreateAnimalCommandHandlerTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task Handle_ValidCommand_ShouldCallRepository()
    {
        // Arrange
        var repository = Substitute.For<IAnimalRepository>();
        var validator = new InlineValidator<CreateAnimalCommand>();
        var handler = new CreateAnimalCommandHandler(repository, validator);

        var validCommand = _fixture.Build<CreateAnimalCommand>()
            .Create();

        // Act
        await handler.Handle(validCommand, CancellationToken.None);

        // Assert
        await repository.Received(1).CreateOrUpdate(Arg.Is<AnimalDto>(dto =>
            dto.Name == validCommand.Name));
    }
}
