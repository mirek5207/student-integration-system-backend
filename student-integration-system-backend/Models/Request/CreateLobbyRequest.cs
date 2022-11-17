using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class CreateLobbyRequest
{
    public int MaxSeats { get; set; }
    public string Name { get; set; }
    public LobbyType Type { get; set; }
    public int? PlaceId { get; set; }
    public int? CustomPlaceId { get; set; }
    public int OwnerId { get; set; }
}

public class CreateLobbyRequestValidator : AbstractValidator<CreateLobbyRequest>
{
    public CreateLobbyRequestValidator()
    {
        RuleFor(l => l.MaxSeats)
            .GreaterThan(0).WithMessage("MaxSeats have to be greater than 0");
        RuleFor(l => l.Name)
            .NotNull().WithMessage("Name is required");
        RuleFor(l => l.Type)
            .IsInEnum().WithMessage("Lobby type have to be in enum range");
        When(l => l.PlaceId != null, () =>
        {
            RuleFor(l => l.PlaceId)
                .GreaterThan(0).WithMessage("PlaceId have to be greater than 0");
        });
        When(l => l.CustomPlaceId != null, () =>
        {
            RuleFor(l => l.CustomPlaceId)
                .GreaterThan(0).WithMessage("CustomPlaceId have to be greater than 0");
        });
        RuleFor(l => l.OwnerId)
            .NotEmpty().WithMessage("OwnerId is required");
    }
}