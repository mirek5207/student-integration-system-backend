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
    /// Creates new moderator. Available for: Admin
    /// </summary>
    [HttpPost("createModeratorAccount")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<AuthenticationResponse> CreateModerator(ModeratorSignUpRequest request)
    {
        var response = _moderatorService.RegisterModerator(request);
        return Ok(response);
    }
    /// <summary>
    /// Update moderator data. Available for: Admin
    /// </summary>
    [HttpPatch("updateModeratorAccount/{moderatorId:int}")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Moderator> UpdateModerator(UpdateModeratorRequest request, int moderatorId)
    {
        var response = _moderatorService.UpdateModerator(request, moderatorId);
        return Ok(response);
    }
    
    /// <summary>
    /// Get moderator by id. Available for: Admin
    /// </summary>
    [HttpGet("getModerator/{moderatorId:int}")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Moderator> GetModeratorById(int moderatorId)
    {
        var response = _moderatorService.GetModeratorById(moderatorId);
        return Ok(response);
    }
    
    /// <summary>
    /// Get all moderators. Available for: Admin
    /// </summary>
    [HttpGet("getModerators")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<List<Moderator>> GetModerators()
    {
        var response = _moderatorService.GetAllModerators();
        return Ok(response);
    }

    /// <summary>
    /// Get all system reports. Available for: Admin
    /// </summary>
    [HttpGet("getSystemReports")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<List<Report>> GetAllSystemReports()
    {
        var response = _reportService.GetAllReportsByReportType(ReportType.SystemErrorReport);
        return Ok(response);
    }

    /// <summary>
    /// Update status of system report(Unverified,InProgress,Verified). Available for: Admin
    /// </summary>
    [HttpPatch("updateStatusOfSystemReport/{reportId:int}")]
    [Authorize(Roles = RoleType.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Report> UpdateStatusOfSystemReport(int reportId, UpdateStatusOfReportRequest updateStatusOfReportRequest)
    {
        var response = _reportService.UpdateStatusOfReport(reportId, updateStatusOfReportRequest._reportStatus);
        return response;
    }
}