using FluentValidation;

namespace student_integration_system_backend.Models.Request;

public class UpdatePlaceRequest
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int NumberOfReservedSeats { get; set; }
    public int MaxSeatsAvailableForReservation { get; set; }
}

public class UpdatePlaceRequestValidator : AbstractValidator<UpdatePlaceRequest>
{
    public UpdatePlaceRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required");
        RuleFor(p => p.Latitude)
            .NotNull().WithMessage("Latitude is required");
        RuleFor(p => p.Longitude)
            .NotNull().WithMessage("Longitude is required");
        RuleFor(p => p.NumberOfReservedSeats)
            .NotNull().WithMessage("Number of reserved seats is required")
            .LessThanOrEqualTo(p => p.MaxSeatsAvailableForReservation).WithMessage("Number of reserved seats have to be lesser than or equal max seats available for reservation");
        RuleFor(p => p.MaxSeatsAvailableForReservation)
            .GreaterThanOrEqualTo(0).WithMessage("MaxSeatsAvailableForReservation have to be greater than or equal 0");
    }
}