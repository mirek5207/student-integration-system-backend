using FluentValidation;

namespace student_integration_system_backend.Models.Request;

public class UpdatePlaceRequest
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
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
    }
}