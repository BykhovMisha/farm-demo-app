using AutoFixture;
using FarmDemoApp.BL.Handlers.Animal;
using FarmDemoApp.BL.Validators.Animal;
using FarmDemoApp.DataAccess.Abstracts.Repository;
using FarmDemoApp.DataAccess.Dto.Animal;
using NSubstitute;

namespace FarmDemoApp.BL.Tests.Handlers.Animal;

public class UpdateAnimalCommandHandlerTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task Handle_ValidCommand_ShouldCallRepository()
    {
        // Arrange
        var repository = Substitute.For<IAnimalRepository>();
        var validator = Substitute.For<UpdateAnimalCommandValidator>();
        var handler = new UpdateAnimalCommandHandler(repository, validator);
        
        var validCommand = _fixture.Build<UpdateAnimalCommand>()
            .Create();

        // Act
        await handler.Handle(validCommand, CancellationToken.None);

        // Assert
        await repository.Received(1).CreateOrUpdate(Arg.Is<AnimalDto>(dto =>
                dto.Id == validCommand.Id && dto.Name == validCommand.Name));
    }
}
