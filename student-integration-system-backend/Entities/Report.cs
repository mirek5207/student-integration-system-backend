using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace student_integration_system_backend.Entities;

public class Report
{
    public int Id { get; set; }
    public ReportType ReportType { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public ReportStatus Status { get; set; }
    public int? ReportedUserId { get; set; }
    public int ReportingUserId { get; set; }
    
    [ForeignKey("ReportedUserId")]
    public virtual User ReportedUser { get; set; }
    [ForeignKey("ReportingUserId")]
    public virtual User ReportingUser { get; set; }
}

public enum ReportType
{
    UserReport,
    SystemErrorReport
}

public enum ReportStatus
{
    Verified,
    InProgress, 
    Unverified
}