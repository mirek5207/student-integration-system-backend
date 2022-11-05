using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class UpdateModeratorRequest
{
    public string Login { get; set; }
    public string Email { get; set; }
    public string FirstName{ get; set; }
    public string SurName { get; set; }
    public string HashedPassword { get; set; }
    public bool IsAccountActive { get; set; }
}
public class UpdateModeratorRequestValidator: AbstractValidator<UpdateModeratorRequest>
{
    public UpdateModeratorRequestValidator(AppDbContext dbContext)
    {
        
        RuleFor(e => e.Login)
            .MinimumLength(5).WithMessage("Login must be at least 4 characters long");
        
        RuleFor(e => e.Email)
            .EmailAddress().WithMessage("Email is not valid");
        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage("First name is required");
        RuleFor(e =>e.SurName)
            .NotEmpty().WithMessage("Surname is required");
        RuleFor(e => e.HashedPassword)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .Matches("[A-Z]").WithMessage("Password must have at least 1 upper case character")
            .Matches("[a-z]").WithMessage("Password must have at least 1 lower case character")
            .Matches("[0-9]").WithMessage("Password must have at least 1 digit")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must have at least 1 special character");
        RuleFor(e => e.IsAccountActive)
            .NotEmpty().WithMessage("IsAccountActive is required");

    }
}