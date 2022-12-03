using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class CreateLobbyAtCustomPlaceRequest
{
    public int MaxSeats { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public LobbyType Type { get; set; }
    public int? CustomPlaceId { get; set; }
    public string CustomPlaceName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Description { get; set; }
}

public class CreateLobbyAtCustomPlaceRequestValidator : AbstractValidator<CreateLobbyAtCustomPlaceRequest>
{
    public CreateLobbyAtCustomPlaceRequestValidator()
    {
        RuleFor(l => l.MaxSeats)
            .GreaterThan(0).WithMessage("MaxSeats have to be greater than 0");
        RuleFor(l => l.Name)
            .NotNull().WithMessage("Name is required");
        RuleFor(l => l.Type)
            .IsInEnum().WithMessage("Lobby type have to be in enum range");
        RuleFor(l => l.StartDate)
            .NotNull().WithMessage("Start date is required");
        When(l => l.CustomPlaceId != null, () =>
        {
            RuleFor(l => l.CustomPlaceId)
                .GreaterThan(0).WithMessage("CustomPlaceId have to be greater than 0");
        });
        RuleFor(l => l.CustomPlaceName)
            .NotNull().WithMessage("Custom place name is required");
        RuleFor(l => l.Latitude)
            .NotNull().WithMessage("Latitude is required");
        RuleFor(l => l.Longitude)
            .NotNull().WithMessage("Longitude is required");
    }
}