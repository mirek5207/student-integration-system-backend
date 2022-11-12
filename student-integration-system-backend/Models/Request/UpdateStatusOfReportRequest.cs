using FluentValidation;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Request;

public class UpdateStatusOfReportRequest
{
    public ReportStatus _reportStatus { get; set; }
}

public class UpdateStatusOfReportRequestValidator : AbstractValidator<UpdateStatusOfReportRequest>
{
    public UpdateStatusOfReportRequestValidator()
    {
        RuleFor(r => r._reportStatus)
            .NotNull().WithMessage("ReportStatus is required");
    }
}