using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Services.LobbyService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LobbyController : ControllerBase
{
    private readonly ILobbyService _lobbyService;

    public LobbyController(ILobbyService lobbyService)
    {
        _lobbyService = lobbyService;
    }
    
    /// <summary>
    /// Creates new lobby
    /// </summary>
    [HttpPost("createLobby{userId:int}")]
    public ActionResult<Lobby> CreateLobby(CreateLobbyRequest request, int userId)
    {
        var lobby = _lobbyService.CreateLobby(request, userId);
        return Ok(lobby);
    }
}