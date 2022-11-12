using FluentValidation;

namespace student_integration_system_backend.Models.Request;

public class UpdateCustomPlaceRequest
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Description { get; set; }
}
public class UpdateCustomPlaceRequestValidator : AbstractValidator<UpdateCustomPlaceRequest>
{
    public UpdateCustomPlaceRequestValidator()
    {
        RuleFor(p => p.Latitude)
            .NotNull().WithMessage("Latitude is required");
        RuleFor(p => p.Longitude)
            .NotNull().WithMessage("Longitude is required");
    }
}