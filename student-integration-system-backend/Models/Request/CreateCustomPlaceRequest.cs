using FluentValidation;

namespace student_integration_system_backend.Models.Request;

public class CreateCustomPlaceRequest
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Description { get; set; }
}
public class CreateCustomPlaceRequestValidator : AbstractValidator<CreateCustomPlaceRequest>
{
    public CreateCustomPlaceRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("Name is required");
        RuleFor(p => p.Latitude)
            .NotNull().WithMessage("Latitude is required");
        RuleFor(p => p.Longitude)
            .NotNull().WithMessage("Longitude is required");
    }
}