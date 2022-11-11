using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.AccountService;
using student_integration_system_backend.Services.ClientService;
using student_integration_system_backend.Services.PlaceOwnerService;
using student_integration_system_backend.Services.Reports;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModeratorController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IAccountService _accountService;
    private readonly IClientService _clientService;
    private readonly IPlaceOwnerService _placeOwnerService;
    public ModeratorController(IReportService reportService, IAccountService accountService, IClientService clientService, IPlaceOwnerService placeOwnerService)
    {
        _reportService = reportService;
        _accountService = accountService;
        _clientService = clientService;
        _placeOwnerService = placeOwnerService;
    }
    
    /// <summary>
    /// Get all user reports. Available for: Moderator
    /// </summary>
    [HttpGet("getUserReports")]
    [Authorize(Roles = RoleType.Moderator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<List<Report>> GetAllUserReports()
    {
        var response = _reportService.GetAllReportsByReportType(ReportType.UserReport);
        return Ok(response);
    }
    
    /// <summary>
    /// Update status of user report(Unverified,InProgress,Verified). Available for: Moderator
    /// </summary>
    [HttpPatch("updateStatusOfUserReport/{reportId:int}/{reportStatus}")]
    [Authorize(Roles = RoleType.Moderator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Report> UpdateStatusOfUserReport(int reportId, ReportStatus reportStatus)
    {
        var response = _reportService.UpdateStatusOfReport(reportId, reportStatus);
        return response;
    }
    /// <summary>
    /// Update status of user account.(IsActive: true,false) Available for: Moderator
    /// </summary>
    [HttpPatch("UpdateStatusOfUserAccount/{userId:int}/{isActive}")]
    [Authorize(Roles = RoleType.Moderator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Account> UpdateStatusOfUserAccount(int userId, bool isActive)
    {
        var response = _accountService.UpdateStatusOfUserAccount(userId, isActive);
        return Ok(response);
    }
    
    /// <summary>
    /// Get all clients. Available for: Moderator
    /// </summary>
    [HttpGet("getAllClients")]
    [Authorize(Roles = RoleType.Moderator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<List<Client>> GetAllClients()
    {
        var response = _clientService.GetAllClients();
        return Ok(response);
    }
    
    /// <summary>
    /// Get all place owner. Available for: Moderator
    /// </summary>
    [HttpGet("getAllPlaceOwner")]
    [Authorize(Roles = RoleType.Moderator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<List<PlaceOwner>> GetAllPlaceOwners()
    {
        var response = _placeOwnerService.GetAllPlaceOwners();
        return Ok(response);
    }
    
}