using FluentValidation;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
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
        RuleFor(u => u.Login)
            .Must((login) =>
            {
                var user = dbContext.Users.Where(user => user.Login == login);
                return user.Any();
            }).WithMessage("Login does not exist")
            .Must((login) =>
            {
                var user = dbContext.Users.FirstOrDefault(user => user.Login == login);
                var account = dbContext.Accounts.FirstOrDefault(account => account.User.Equals(user));
                if (account == null) throw new NotFoundException("User account not found");
                return account.IsActive;
            }).WithMessage("Account is not active");
        

        RuleFor(u => new {u.Password, u.Login}).Must((u) =>
        {
            
            var user = dbContext.Users.FirstOrDefault(us => us.Login == u.Login);
            
            return BCrypt.Net.BCrypt.Verify(u.Password,user.HashedPassword);
        })
        .WithMessage("Password is incorrect");
    }
}