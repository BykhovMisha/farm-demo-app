using AutoFixture;
using FarmDemoApp.API.Controllers;
using FarmDemoApp.API.Models.ApiModels;
using FarmDemoApp.BL;
using FarmDemoApp.BL.Handlers.Animal;
using FarmDemoApp.BL.Models.Common;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace FarmDemoApp.API.Tests.Controllers;

public class AnimalControllerTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task GetAnimals_ReturnsOk_WhenQueryIsValid()
    {
        // Arrange
        var sender = Substitute.For<ISender>();
        var controller = new AnimalController(sender);
        var queryModel = _fixture.Build<GetVisitsApiModel>().Create();
        var queryResult = _fixture.Build<PageModel<AnimalModel>>().Create();
        sender.Send(Arg.Any<GetAnimalPageQuery>()).Returns(queryResult);

        // Act
        var result = await controller.GetAnimals(queryModel, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.Should().Be(queryResult);
    }

    [Fact]
    public async Task CreateAnimal_ReturnsOk_WhenCommandIsValid()
    {
        // Arrange
        var sender = Substitute.For<ISender>();
        var controller = new AnimalController(sender);
        var createModel = _fixture.Build<CreateUpdateAnimalApiModel>().Create();
        sender.Send(Arg.Any<CreateAnimalCommand>()).Returns(Task.CompletedTask);

        // Act
        var result = await controller.CreateAnimal(createModel);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task UpdateAnimal_ReturnsOk_WhenCommandIsValid()
    {
        // Arrange
        var sender = Substitute.For<ISender>();
        var controller = new AnimalController(sender);
        var id = 1;
        var updateModel = _fixture.Build<CreateUpdateAnimalApiModel>().Create();
        sender.Send(Arg.Any<UpdateAnimalCommand>()).Returns(Task.CompletedTask);

        // Act
        var result = await controller.UpdateAnimal(id, updateModel);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task DeleteAnimal_ReturnsOk_WhenCommandIsValid()
    {
        // Arrange
        var sender = Substitute.For<ISender>();
        var controller = new AnimalController(sender);
        var id = 1;
        sender.Send(Arg.Any<DeleteAnimalCommand>()).Returns(Task.CompletedTask);

        // Act
        var result = await controller.DeleteAnimal(id);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
}
