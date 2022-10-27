using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Data.Import;

public class ReportImport : IDataImport
{
    public void Seed(ModelBuilder builder)
    {
        var reports = new List<Report>
        {
            new()
            {
                Id = 1,
                CreationDate = DateTime.Now,
                ReportType = ReportType.UserReport,
                Description = "User Report",
                Status = ReportStatus.Unverified,
                ReportedUserId = 2,
                ReportingUserId = 1
            },
            new()
            {
                Id = 2,
                CreationDate = DateTime.Now,
                ReportType = ReportType.SystemErrorReport,
                Description = "System Error #1",
                Status = ReportStatus.Unverified,
                ReportingUserId = 2
            },
            new()
            {
                Id = 3,
                CreationDate = DateTime.Now,
                ReportType = ReportType.SystemErrorReport,
                Description = "System Error #2",
                Status = ReportStatus.Unverified,
                ReportingUserId = 2
            }
        };
        builder.Entity<Report>().HasData(reports);
    }
}