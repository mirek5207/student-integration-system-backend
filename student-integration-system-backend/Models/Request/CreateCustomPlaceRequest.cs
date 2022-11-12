using FluentValidation;

namespace student_integration_system_backend.Models.Request;

public class CreateCustomPlaceRequest
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
}
public class CreateCustomPlaceRequestValidator : AbstractValidator<CreateCustomPlaceRequest>
{
    public CreateCustomPlaceRequestValidator()
    {
        RuleFor(p => p.Latitude)
            .NotNull().WithMessage("Latitude is required");
        RuleFor(p => p.Longitude)
            .NotNull().WithMessage("Longitude is required");
        RuleFor(p => p.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}