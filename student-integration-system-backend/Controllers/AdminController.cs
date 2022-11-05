using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.ModeratorService;
using student_integration_system_backend.Services.Reports;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IModeratorService _moderatorService;
    private readonly IReportService _reportService;

    public AdminController(IModeratorService moderatorService, IReportService reportService)
    {
        _moderatorService = moderatorService;
        _reportService = reportService;
    }
    
    /// <summary>
    /// Creates new moderator
    /// </summary>
    [HttpPost("createModeratorAccount")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<AuthenticationResponse> CreateModerator(ModeratorSignUpRequest request)
    {
        var response = _moderatorService.RegisterModerator(request);
        return Ok(response);
    }
    /// <summary>
    /// Update moderator data
    /// </summary>
    [HttpPatch("updateModeratorAccount")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Moderator> UpdateModerator(UpdateModeratorRequest request, int moderatorId)
    {
        var response = _moderatorService.UpdateModerator(request, moderatorId);
        return Ok(response);
    }
    /// <summary>
    /// Get all moderators
    /// </summary>
    [HttpGet("getModerators")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<List<Moderator>> GetModerators()
    {
        var response = _moderatorService.GetAllModerators();
        return Ok(response);
    }

    /// <summary>
    /// Get all system reports
    /// </summary>
    [HttpGet("getSystemReports")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<List<Report>> GetAllSystemReports()
    {
        var response = _reportService.GetAllReportsByReportType(ReportType.SystemErrorReport);
        return Ok(response);
    }

    /// <summary>
    /// Update status of system report(Unverified,InProgress,Verified)
    /// </summary>
    [HttpPatch("updateStatusOfSystemReport")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Report> UpdateStatusOfSystemReport(int reportId, ReportStatus reportStatus)
    {
        var response = _reportService.UpdateStatusOfReport(reportId, reportStatus);
        return response;
    }
}