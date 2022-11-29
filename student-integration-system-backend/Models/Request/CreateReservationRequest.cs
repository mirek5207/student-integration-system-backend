using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class CreateReservationRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfGuests { get; set; }
    public int PlaceId { get; set; }
    public int LobbyId { get; set; }
}

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        RuleFor(r => r.StartDate)
            .NotNull().WithMessage("Start date is required");
        RuleFor(r => r.EndDate)
            .NotNull().WithMessage("End date is required");
        RuleFor(r => r.NumberOfGuests)
            .GreaterThan(0).WithMessage("Number of reserved seats must be greater than 0.");
        RuleFor(r => r.PlaceId)
            .NotEmpty().WithMessage("PlaceId is required");
        RuleFor(r => r.LobbyId)
            .NotEmpty().WithMessage("LobbyId is required");
    }
}