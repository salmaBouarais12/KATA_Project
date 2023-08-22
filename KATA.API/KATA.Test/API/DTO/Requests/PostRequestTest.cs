using System.ComponentModel.DataAnnotations;
using KATA.API.DTO.Requests;
using NFluent;

namespace KATA.Test.API.DTO.Requests;

public class PostRequestTest
{
    [Fact]
    public void Should_Validate_When_Request_Is_Valid()
    {
        // Arrange
        var request = CreateValidRequest();

        var validationContext = new ValidationContext(request);
        var errors = new List<ValidationResult>();

        // Act
        var result = Validator.TryValidateObject(request, validationContext, errors, true);

        // Assert
        Check.That(result).IsTrue();
    }

    [Fact]
    public void Should_Not_Validate_When_FirstName_Is_Empty()
    {
        // Arrange
        var request = CreateValidRequest();
        request.FirstName = string.Empty;

        var validationContext = new ValidationContext(request);
        var errors = new List<ValidationResult>();

        // Act
        var result = Validator.TryValidateObject(request, validationContext, errors, true);

        // Assert
        Check.That(result).IsFalse();
        Check.That(errors).HasSize(1);
        Check.That(errors.Single().MemberNames).HasSize(1);
        Check.That(errors.Single().MemberNames.Single()).IsEqualTo(nameof(request.FirstName));
    }

    [Fact]
    public void Should_Not_Validate_When_LastName_Is_Empty()
    {
        // Arrange
        var request = CreateValidRequest();
        request.LastName = string.Empty;

        var validationContext = new ValidationContext(request);
        var errors = new List<ValidationResult>();

        // Act
        var result = Validator.TryValidateObject(request, validationContext, errors, true);

        // Assert
        Check.That(result).IsFalse();
        Check.That(errors).HasSize(1);
        Check.That(errors.Single().MemberNames).HasSize(1);
        Check.That(errors.Single().MemberNames.Single()).IsEqualTo(nameof(request.LastName));
    }

    [Fact]
    public void Should_Not_Validate_When_RoomName_Is_Empty()
    {
        // Arrange
        var request = CreateRoomValidRequest();
        request.RoomName = string.Empty;

        var validationContext = new ValidationContext(request);
        var errors = new List<ValidationResult>();

        // Act
        var result = Validator.TryValidateObject(request, validationContext, errors, true);

        // Assert
        Check.That(result).IsFalse();
        Check.That(errors).HasSize(1);
        Check.That(errors.Single().MemberNames).HasSize(1);
        Check.That(errors.Single().MemberNames.Single()).IsEqualTo(nameof(request.RoomName));
    }

    private PostPersonRequest CreateValidRequest() =>
        new()
        {
            FirstName = "Martin",
            LastName = "Dupond"
        };
    private PostRoomRequest CreateRoomValidRequest() =>
        new()
        {
            RoomName = "White Room"
        };
    /*
     On teste si c'est valide avec le cas minimum
     On teste chaque erreur
     */
}
