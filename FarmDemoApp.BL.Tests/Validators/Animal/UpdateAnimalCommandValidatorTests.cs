using FarmDemoApp.BL.Handlers.Animal;
using FarmDemoApp.BL.Validators.Animal;
using FluentAssertions;

namespace FarmDemoApp.BL.Tests.Validators.Animal;

public class UpdateAnimalCommandValidatorTests
{
    private readonly UpdateAnimalCommandValidator _validator = new UpdateAnimalCommandValidator();

    [Fact]
    public void Validate_ValidCommand_ShouldNotHaveErrors()
    {
        // Arrange
        var command = new UpdateAnimalCommand { Id = 1, Name = "Cow" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    //[MemberData(nameof(InvalidModelTestCases))]
    public void Validate_InvalidCommand_ShouldHaveErrors()
    {
        // Arrange
        var command = new UpdateAnimalCommand { Id = 0, Name = "Cow" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be("Id should be greater than 0");
    }

    public static TheoryData<int, string, string> InvalidModelTestCases => new TheoryData<int, string, string>
        {
            { 0, "Cow", "Id should be greater than 0" },
            { 1, "", "Name should not be empty" },
            { 1, "X".PadRight(201, 'X'), "Name maximum length is 200" },
        };
}
