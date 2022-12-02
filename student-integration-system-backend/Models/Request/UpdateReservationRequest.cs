using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class UpdateReservationRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfGuests { get; set; }
    public string PhoneNumber { get; set; }
    public int LobbyId { get; set; }
}

public class UpdateReservationRequestValidator : AbstractValidator<UpdateReservationRequest>
{
    public UpdateReservationRequestValidator(AppDbContext dbContext)
    {
        RuleFor(r => r.StartDate)
            .NotNull().WithMessage("Start date is required");
        RuleFor(r => r.EndDate)
            .NotNull().WithMessage("End date is required");
        RuleFor(r => r.PhoneNumber)
            .NotNull().WithMessage("Phone is required");
        RuleFor(r => r.NumberOfGuests)
            .GreaterThan(0).WithMessage("Number of reserved seats must be greater than 0.");
        RuleFor(r => new {r.NumberOfGuests, r.LobbyId})
            .Must((r) =>
            {
                var maxSeats = dbContext.Lobbies.Where(l => l.Id == r.LobbyId).Select(l => l.MaxSeats).First();
                return maxSeats >= r.NumberOfGuests;
            }).WithMessage("Number of reserved seats must be less or equal than maximum seats in lobby.");
        RuleFor(r => r.LobbyId)
            .NotEmpty().WithMessage("LobbyId is required");
    }
}