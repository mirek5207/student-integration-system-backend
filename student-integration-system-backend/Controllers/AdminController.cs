using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.ModeratorService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IModeratorService _moderatorService;

    public AdminController(IModeratorService moderatorService)
    {
        _moderatorService = moderatorService;
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
}