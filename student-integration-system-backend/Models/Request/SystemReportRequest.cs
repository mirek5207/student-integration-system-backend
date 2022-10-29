using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class SystemReportRequest
{
    public string Description { get; set; }
    public int ReportingUserId { get; set; }
}

public class SystemReportRequestValidator : AbstractValidator<SystemReportRequest>
{
    public SystemReportRequestValidator(AppDbContext dbContext)
    {
        RuleFor(e => e.ReportingUserId)
            .NotEmpty().WithMessage("Reporting user id is required");
        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required");
    }
}