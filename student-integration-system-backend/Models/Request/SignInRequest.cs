using FluentValidation;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Models.Request;

public class SignInRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    
}

public class SignInRequestValidator : AbstractValidator<SignInRequest>
{
    public SignInRequestValidator(AppDbContext dbContext)
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(u => u.Login).Must((login) =>
        {
            var user = dbContext.Users.Where(user => user.Login == login);
            return user.Any();
        })
        .WithMessage("Login does not exist");

        RuleFor(u => new {u.Password, u.Login}).Must((u) =>
        {
            
            var user = dbContext.Users.FirstOrDefault(us => us.Login == u.Login);
            
            return BCrypt.Net.BCrypt.Verify(u.Password,user.HashedPassword);
        })
        .WithMessage("Password is incorrect");
    }
}