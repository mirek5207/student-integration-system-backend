using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class UserReportRequest
{
    public string Description { get; set; }
    public int ReportingUserId { get; set; }
    public string ReportedUserLogin { get; set; }
}
public class UserReportRequestValidator : AbstractValidator<UserReportRequest>
{
    public UserReportRequestValidator(AppDbContext dbContext)
    {
        RuleFor(e => e.ReportingUserId)
            .NotEmpty().WithMessage("Reporting user id is required"); 
        RuleFor(e => e.ReportedUserLogin)
            .NotEmpty().WithMessage("Reported user login is required");
        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required");
    }
}