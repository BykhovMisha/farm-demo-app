using FarmDemoApp.BL.Handlers.Animal.Abstracts;
using FarmDemoApp.BL.Validators.Animal;
using FluentAssertions;

namespace FarmDemoApp.BL.Tests.Validators.Animal;

public class AnimalCommandWithNameAbstractValidatorTests
{
    private readonly FakeCommandWithNameValidator _validator = new FakeCommandWithNameValidator();

    [Fact]
    public void Validate_ValidCommand_ShouldNotHaveErrors()
    {
        // Arrange
        var command = new FakeCommandWithName { Name = "Cow" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(InvalidModelTestCases))]
    public void Validate_InvalidCommand_ShouldHaveErrors(string name, string errorMessage)
    {
        // Arrange
        var command = new FakeCommandWithName { Name = name };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be(errorMessage);
    }

    public static TheoryData<string, string> InvalidModelTestCases => new TheoryData<string, string>
        {
            { "", "Name should not be empty" },
            { "X".PadRight(201, 'X'), "Name maximum length is 200" },
        };

    private class FakeCommandWithName : IAnimalCommadWithName
    {
        public required string Name { get; init; }
    }

    private class FakeCommandWithNameValidator: AnimalCommandWithNameAbstractValidator<FakeCommandWithName>
    {
    }
}
