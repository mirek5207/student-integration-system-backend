using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Services.ClientService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }


    /// <summary>
    /// Creates new client
    /// </summary>
    [HttpPost("createAccount")]
    public ActionResult<AuthenticationResponse> CreateClient(ClientSignUpRequest request)
    {
        var response = _clientService.RegisterClient(request);
        return Ok(response);
    }
}