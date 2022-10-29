using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Services.Reports;

public interface IReportService
{
    IEnumerable<Report> GetAllReportsByReportType(ReportType reportType);
    Report UpdateStatusOfReport(int reportId, ReportStatus reportStatus);
}