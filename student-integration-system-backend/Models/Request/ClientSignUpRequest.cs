using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class ClientSignUpRequest
{
    public string Login { get; set; }
    public string Email { get; set; }
    public string FirstName{ get; set; }
    public string SurName { get; set; }
    public string HashedPassword { get; set; }
}

public class ClientSignUpRequestValidator: AbstractValidator<ClientSignUpRequest>
{
    public ClientSignUpRequestValidator(AppDbContext dbContext)
    {
        RuleFor(e => e.Login)
            .NotEmpty().WithMessage("Login is required")
            .MinimumLength(5).WithMessage("Login must be at least 4 characters long")
            .Must((login) =>
            {
                var loginExist = dbContext.Users.Any(user => user.Login == login);
                return !loginExist;
            }).WithMessage("Login already exist");
        
        RuleFor(e => e.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid")
            .Must((email) =>
            {
                var emailExist = dbContext.Users.Any(user => user.Email == email);
                return !emailExist;
            }).WithMessage("Email already exist");;
        
        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage("First name is required");
        
        RuleFor(e =>e.SurName)
            .NotEmpty().WithMessage("Surname is required");
        
        RuleFor(e => e.HashedPassword)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .Matches("[A-Z]").WithMessage("Password must have at least 1 upper case character")
            .Matches("[a-z]").WithMessage("Password must have at least 1 lower case character")
            .Matches("[0-9]").WithMessage("Password must have at least 1 digit")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must have at least 1 special character");
    }
}