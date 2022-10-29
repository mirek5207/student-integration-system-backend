using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.Reports;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IReportService _reportService;

    public UserController(IReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>
    /// Creates new system report
    /// </summary>
    [HttpPost("createSystemReport")]
    [Authorize(Roles = RoleType.Moderator + "," + RoleType.Client + "," + RoleType.PlaceOwner , AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Report> CreateSystemReport(SystemReportRequest request)
    {
        var response =  _reportService.CreateSystemReport(request);
        return Ok(response);
    }
}