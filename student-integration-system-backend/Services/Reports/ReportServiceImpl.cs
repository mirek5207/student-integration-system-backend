using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;

namespace student_integration_system_backend.Services.Reports;

public class ReportServiceImpl : IReportService
{
    private readonly AppDbContext _dbContext;

    public ReportServiceImpl(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Report> GetAllReportsByReportType(ReportType reportType)
    {
        var reports = _dbContext.Reports.ToList().Where(report => report.ReportType == reportType );
        if (reports == null) throw new NotFoundException("Not found any reports");
        return reports;
    }

    public Report UpdateStatusOfReport(int reportId, ReportStatus reportStatus)
    {
        var report = GetReportById(reportId);
        report.Status = reportStatus;
        _dbContext.SaveChanges();
        return report;
    }

    private Report GetReportById(int reportId)
    {
        var report = _dbContext.Reports.FirstOrDefault(report => report.Id == reportId);
        if (report == null) throw new NotFoundException("Report not found");
        return report;
    }
}