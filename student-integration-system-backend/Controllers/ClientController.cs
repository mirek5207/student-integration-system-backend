using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.ClientService;
using student_integration_system_backend.Services.CustomPlaceService;
using student_integration_system_backend.Services.UserService;
using Swashbuckle.AspNetCore.Annotations;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IUserService _userService;
    private readonly ICustomPlaceService _placeService;

    public ClientController(IClientService clientService, IUserService userService, ICustomPlaceService placeService)
    {
        _clientService = clientService;
        _userService = userService;
        _placeService = placeService;
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
    
    
    // --------------------------- Custom Places --------------------------- \\
    
    
    /// <summary>
    /// Create custom place by client. Available for: Client
    /// </summary>
    [HttpPost("createCustomPlace")]
    [SwaggerOperation(Tags = new[] { "Client Custom Place" })]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<CustomPlace> CreateCustomPlace(CreateCustomPlaceRequest request)
    {
        var response = _placeService.CreateCustomPlace(request);
        return Ok(response);
    }
    
    /// <summary>
    /// Delete custom place by client. Available for: Client
    /// </summary>
    [HttpDelete("deleteCustomPlace/{customPlaceId:int}")]
    [SwaggerOperation(Tags = new[] { "Client Custom Place" })]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult DeleteCustomPlace(int customPlaceId)
    {
        _placeService.DeleteCustomPlace(customPlaceId);
        return NoContent();
    }
    
    /// <summary>
    /// Update custom place by client. Available for: Client
    /// </summary>
    [HttpPatch("updateCustomPlace/{customPlaceId:int}")]
    [SwaggerOperation(Tags = new[] { "Client Custom Place" })]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<CustomPlace> UpdateCustomPlace(UpdateCustomPlaceRequest request, int customPlaceId)
    {
        var response = _placeService.UpdateCustomPlace(customPlaceId, request);
        return response;
    }
}