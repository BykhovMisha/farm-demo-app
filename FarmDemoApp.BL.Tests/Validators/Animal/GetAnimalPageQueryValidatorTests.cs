using FarmDemoApp.BL.Handlers.Animal;
using FarmDemoApp.BL.Validators.Animal;
using FluentAssertions;

namespace FarmDemoApp.BL.Tests.Validators.Animal;

public class GetAnimalPageQueryValidatorTests
{
    public readonly GetAnimalPageQueryValidator _validator;

    public GetAnimalPageQueryValidatorTests()
    {
        _validator = new GetAnimalPageQueryValidator();
    }

    [Fact]
    public void Validate_ValidQuery_ShouldNotHaveErrors()
    {
        // Arrange
        var query = new GetAnimalPageQuery
        {
            Skip = 0,
            Take = 10,
            Name = "Cow"
        };

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(InvalidQueryTestCases))]
    public void Validate_InvalidQuery_ShouldHaveErrors(GetAnimalPageQuery query, string expectedErrorMessage)
    {
        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be(expectedErrorMessage);
    }

    public static TheoryData<GetAnimalPageQuery, string> InvalidQueryTestCases => new TheoryData<GetAnimalPageQuery, string>
        {
            { new GetAnimalPageQuery { Skip = -1, Take = 10, Name = "Cow" }, "Skip should be greater than or equal to 0" },
            { new GetAnimalPageQuery { Skip = 0, Take = 101, Name = "Cow" }, "Take should be from 1 to 100" },
            { new GetAnimalPageQuery { Skip = 0, Take = 10, Name = new string('X', 201) }, "Name maximum length is 200" },
        };
}
