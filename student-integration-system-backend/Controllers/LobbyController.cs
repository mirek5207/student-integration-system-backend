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
    /// Returns lobby by Id
    /// </summary>
    [HttpGet("getLobby{lobbyId:int}")]
    public ActionResult<Lobby> GetLobbyById(int lobbyId)
    {
        var lobby = _lobbyService.GetLobbyById(lobbyId);
        return Ok(lobby);
    }

    /// <summary>
    /// Returns all lobbies
    /// </summary>
    [HttpGet("allLobbies")]
    public ActionResult<IEnumerable<Lobby>> GetAllLobbies()
    {
        var lobbies = _lobbyService.GetAllLobbies();
        return Ok(lobbies);
    }
    
    /// <summary>
    /// Returns all lobbies which belongs to specific lobby owner
    /// </summary>
    [HttpGet("allOwnerLobbies")]
    public ActionResult<IEnumerable<Lobby>> GetAllOwnerLobbies(int userId)
    {
        var lobbies = _lobbyService.GetAllClientLobbies(userId);
        return Ok(lobbies);
    }
    
    /// <summary>
    /// Returns all public lobbies
    /// </summary>
    [HttpGet("allPublicLobbies")]
    public ActionResult<IEnumerable<Lobby>> GetAllPubicLobbies()
    {
        var lobbies = _lobbyService.GetAllPublicLobbies();
        return Ok(lobbies);
    }
    
    /// <summary>
    /// Returns all lobbies where user is as guest
    /// </summary>
    [HttpGet("allGuestLobbies")]
    public ActionResult<IEnumerable<Lobby>> GetAllGuestLobbies(int userId)
    {
        var lobbies = _lobbyService.GetAllLobbiesWhereClientIsGuest(userId);
        return Ok(lobbies);
    }
    
    /// <summary>
    /// Returns all lobby invites
    /// </summary>
    [HttpGet("allLobbyInvites")]
    public ActionResult<IEnumerable<Lobby>> GetAllLobbyInvites(int userId)
    {
        var lobbies = _lobbyService.GetAllLobbiesWhereClientIsInvited(userId);
        return Ok(lobbies);
    }
        
    /// <summary>
    /// Creates new lobby
    /// </summary>
    [HttpPost("createLobby")]
    public ActionResult<Lobby> CreateLobby(CreateLobbyRequest request, int userId)
    {
        var lobby = _lobbyService.CreateLobby(request, userId);
        return Ok(lobby);
    }
    
    /// <summary>
    /// Adds guest to lobby
    /// </summary>
    [HttpPut("joinLobby{userId:int}")]
    public ActionResult<string> AddGuestToLobby(int userId, int lobbyId)
    {
        var message = _lobbyService.AddGuestToLobby(userId, lobbyId);
        return Ok(message);
    }

    /// <summary>
    /// Adds guest to lobby
    /// </summary>
    [HttpPost("inviteGuest")]
    public ActionResult<string> InviteGuestToLobby(int userId, int lobbyId)
    {
        var message = _lobbyService.InviteGuestToLobby(userId, lobbyId);
        return Ok(message);
    }

    /// <summary>
    /// Removes guest from lobbyGuests array
    /// </summary>
    [HttpDelete("leaveLobby")]
    public ActionResult<string> RejectInviteOrLeaveLobby(int userId, int lobbyId)
    {
        var message = _lobbyService.RejectInviteOrLeaveLobby(userId, lobbyId);
        return Ok(message);
    }

    /// <summary>
    /// Accepts invite to the lobby
    /// </summary>
    [HttpPut("acceptInvite")]
    public ActionResult<string> AccpetInviteToLobby(int userId, int lobbyId)
    {
        var message = _lobbyService.AcceptInviteToLobby(userId, lobbyId);
        return Ok(message);
    }
}