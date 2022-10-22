using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Services.AuthService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Return access token to page
    /// </summary>
    [HttpPost("authenticate")]
    public ActionResult<AuthenticationResponse> AuthUser(SignInRequest request)
    {
        var authenticationResponse = _authService.AuthUser(request);
        return Ok(authenticationResponse);
    }
}