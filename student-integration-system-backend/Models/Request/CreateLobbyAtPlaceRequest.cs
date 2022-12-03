using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class CreateLobbyAtPlaceRequest
{
    public int MaxSeats { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public LobbyType Type { get; set; }
    public int PlaceId { get; set; }
}

public class CreateLobbyAtPlaceRequestValidator : AbstractValidator<CreateLobbyAtPlaceRequest>
{
    public CreateLobbyAtPlaceRequestValidator()
    {
        RuleFor(l => l.MaxSeats)
            .GreaterThan(0).WithMessage("MaxSeats have to be greater than 0");
        RuleFor(l => l.Name)
            .NotNull().WithMessage("Name is required");
        RuleFor(l => l.Type)
            .IsInEnum().WithMessage("Lobby type have to be in enum range");
        RuleFor(l => l.StartDate)
            .NotNull().WithMessage("Start date is required");
        RuleFor(l => l.PlaceId)
            .NotNull().WithMessage("Place id is required");
    }
}