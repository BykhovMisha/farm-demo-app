using AutoFixture;
using FarmDemoApp.BL.Handlers.Animal;
using FarmDemoApp.BL.Validators.Animal;
using FarmDemoApp.DataAccess.Abstracts.Repository;
using FluentValidation;
using NSubstitute;

namespace FarmDemoApp.BL.Tests.Handlers.Animal;

public class DeleteAnimalCommandHandlerTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task Handle_ValidCommand_ShouldCallRepository()
    {
        // Arrange
        var repository = Substitute.For<IAnimalRepository>();
        var validator = Substitute.For<DeleteAnimalCommandValidator>();
        var handler = new DeleteAnimalCommandHandler(repository, validator);

        var validCommand = _fixture.Create<DeleteAnimalCommand>();

        // Act
        await handler.Handle(validCommand, CancellationToken.None);

        // Assert
        await repository.Received(1).Delete(Arg.Is(validCommand.Id));
    }
}
