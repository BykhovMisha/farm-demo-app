using AutoFixture;
using FarmDemoApp.BL.Handlers.Animal;
using FarmDemoApp.BL.Validators.Animal;
using FarmDemoApp.DataAccess.Abstracts.Repository;
using FarmDemoApp.DataAccess.Dto.Animal;
using FarmDemoApp.DataAccess.Dto.Common;
using FluentAssertions;
using NSubstitute;

namespace FarmDemoApp.BL.Tests.Handlers.Animal;

public class GetAnimalPageQueryHandlerTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task Handle_ValidQuery_ShouldCallRepositoryAndMapDto()
    {
        // Arrange
        var repository = Substitute.For<IAnimalRepository>();
        var validator = Substitute.For<GetAnimalPageQueryValidator>();
        var handler = new GetAnimalPageQueryHandler(repository, validator);

        var validQuery = _fixture.Create<GetAnimalPageQuery>();
        var pageDto = _fixture.Create<PageDto<AnimalDto>>();

        repository.GetPage(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(pageDto);

        // Act
        var result = await handler.Handle(validQuery, CancellationToken.None);

        // Assert
        await repository.Received(1).GetPage(
            Arg.Is(validQuery.Skip),
            Arg.Is(validQuery.Take),
            Arg.Is(validQuery.Name),
            Arg.Any<CancellationToken>());

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(pageDto);
    }
}
