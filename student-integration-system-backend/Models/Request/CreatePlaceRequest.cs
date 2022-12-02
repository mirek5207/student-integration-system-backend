using FluentValidation;

namespace student_integration_system_backend.Models.Request;

public class CreatePlaceRequest
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int UserId { get; set; }
}

public class CreatePlaceRequestValidator : AbstractValidator<CreatePlaceRequest>
{
    public CreatePlaceRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required");
        RuleFor(p => p.Latitude)
            .NotNull().WithMessage("Latitude is required");
        RuleFor(p => p.Longitude)
            .NotNull().WithMessage("Longitude is required");
        RuleFor(p => p.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}