using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.ClientService;
using student_integration_system_backend.Services.UserService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IUserService _userService;

    public ClientController(IClientService clientService, IUserService userService)
    {
        _clientService = clientService;
        _userService = userService;
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

    ///<summary>
    /// Get client data
    /// </summary>
    [HttpGet("getClientData/{clientId:int}")]
    public ActionResult<Client> GetClient(int clientId)
    {
        var client = _clientService.GetClientById(clientId);
        return Ok(client);
    }

    /// <summary>
    /// Update client data
    /// </summary>
    [HttpPatch("updateClientAccount/{clientId:int}")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Client> UpdateClient(UpdateClientRequest request, int clientId)
    {
        var response = _clientService.UpdateClient(request, clientId);
        return Ok(response);
    }
}