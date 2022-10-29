using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class UserReportRequest
{
    public string Description { get; set; }
    public int ReportingUserId { get; set; }
    public int ReportedUserId { get; set; }
}
public class UserReportRequestValidator : AbstractValidator<UserReportRequest>
{
    public UserReportRequestValidator(AppDbContext dbContext)
    {
        RuleFor(e => e.ReportingUserId)
            .NotEqual(e => e.ReportedUserId).WithMessage("Reporting user id is the same as reported user id")
            .NotEmpty().WithMessage("Reporting user id is required"); 
        RuleFor(e => e.ReportedUserId)
            .NotEmpty().WithMessage("Reported user id is required");
        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required");
    }
}