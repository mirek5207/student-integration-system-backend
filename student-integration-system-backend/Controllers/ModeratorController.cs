using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.Reports;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModeratorController : ControllerBase
{
    private readonly IReportService _reportService;

    public ModeratorController(IReportService reportService)
    {
        _reportService = reportService;
    }
    
    /// <summary>
    /// Get all user reports
    /// </summary>
    [HttpGet("getUserReports")]
    [Authorize(Roles = RoleType.Moderator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<List<Report>> GetAllUserReports()
    {
        var response = _reportService.GetAllReportsByReportType(ReportType.UserReport);
        return Ok(response);
    }
    
    /// <summary>
    /// Update status of user report(Unverified,InProgress,Verified)
    /// </summary>
    [HttpPatch("updateStatusOfUserReport")]
    [Authorize(Roles = RoleType.Moderator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Report> UpdateStatusOfUserReport(int reportId, ReportStatus reportStatus)
    {
        var response = _reportService.UpdateStatusOfReport(reportId, reportStatus);
        return response;
    }
}