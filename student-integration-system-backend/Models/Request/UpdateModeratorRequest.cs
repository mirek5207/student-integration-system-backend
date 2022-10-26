using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class UpdateModeratorRequest
{
    public string? Login { get; set; }
    public string? Email { get; set; }
    public string? FirstName{ get; set; }
    public string? SurName { get; set; }
    public string? HashedPassword { get; set; }
    public bool? IsAccountActive { get; set; }
}
public class UpdateModeratorRequestValidator: AbstractValidator<UpdateModeratorRequest>
{
    public UpdateModeratorRequestValidator(AppDbContext dbContext)
    {
        RuleFor(e => e.Login)
            .MinimumLength(5).When(x => !string.IsNullOrEmpty(x.Login)).WithMessage("Login must be at least 4 characters long")
            .Must((login) =>
            {
                var loginExist = dbContext.Users.Any(user => user.Login == login);
                return !loginExist;
            }).When(x => !string.IsNullOrEmpty(x.Login)).WithMessage("Login already exist");
        
        RuleFor(e => e.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email is not valid")
            .Must((email) =>
            {
                var emailExist = dbContext.Users.Any(user => user.Email == email);
                return !emailExist;
            }).When(x => !string.IsNullOrEmpty(x.Login)).WithMessage("Email already exist");

        RuleFor(e => e.HashedPassword)
            .MinimumLength(6).When(x => !string.IsNullOrEmpty(x.HashedPassword)).WithMessage("Password must be at least 6 characters long")
            .Matches("[A-Z]").When(x => !string.IsNullOrEmpty(x.HashedPassword)).WithMessage("Password must have at least 1 upper case character")
            .Matches("[a-z]").When(x => !string.IsNullOrEmpty(x.HashedPassword)).WithMessage("Password must have at least 1 lower case character")
            .Matches("[0-9]").When(x => !string.IsNullOrEmpty(x.HashedPassword)).WithMessage("Password must have at least 1 digit")
            .Matches("[^a-zA-Z0-9]").When(x => !string.IsNullOrEmpty(x.HashedPassword)).WithMessage("Password must have at least 1 special character");
     
    }
}