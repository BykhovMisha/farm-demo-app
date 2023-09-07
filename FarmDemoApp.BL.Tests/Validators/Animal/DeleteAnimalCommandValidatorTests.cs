using FarmDemoApp.BL.Handlers.Animal;
using FarmDemoApp.BL.Validators.Animal;
using FluentAssertions;

namespace FarmDemoApp.BL.Tests.Validators.Animal;

public class DeleteAnimalCommandValidatorTests
{
    private readonly DeleteAnimalCommandValidator _validator = new DeleteAnimalCommandValidator();

    [Fact]
    public void Validate_ValidCommand_ShouldNotHaveErrors()
    {
        // Arrange
        var command = new DeleteAnimalCommand { Id = 1 };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidId_ShouldHaveError()
    {
        // Arrange
        var command = new DeleteAnimalCommand { Id = 0 }; // Invalid Id

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be("Id should be greater than 0");
    }
}
