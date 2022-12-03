using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.CustomPlaceService;
using student_integration_system_backend.Services.LobbyService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LobbyController : ControllerBase
{
    private readonly ILobbyService _lobbyService;
    private readonly ICustomPlaceService _customPlaceService;
    
    public LobbyController(ILobbyService lobbyService, ICustomPlaceService customPlaceService)
    {
        _lobbyService = lobbyService;
        _customPlaceService = customPlaceService;
    }

    /// <summary>
    /// Returns lobby by Id
    /// </summary>
    [HttpGet("getLobby/{lobbyId:int}")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Lobby> GetLobbyById(int lobbyId)
    {
        var lobby = _lobbyService.GetLobbyById(lobbyId);
        return Ok(lobby);
    }
    /// <summary>
    /// Returns guest of lobby by lobbyId
    /// </summary>
    [HttpGet("getLobbyGuests/{lobbyId:int}")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<LobbyGuest> GetLobbyGuests(int lobbyId)
    {
        var response = _lobbyService.GetAllLobbyGuestsForLobby(lobbyId);
        return Ok(response);
    }

    /// <summary>
    /// Returns all lobbies
    /// </summary>
    [HttpGet("allLobbies")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Lobby>> GetAllLobbies()
    {
        var lobbies = _lobbyService.GetAllLobbies();
        return Ok(lobbies);
    }
    
    /// <summary>
    /// Returns all lobbies which belongs to specific lobby owner
    /// </summary>
    [HttpGet("allOwnerLobbies")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Lobby>> GetAllOwnerLobbies(int userId)
    {
        var lobbies = _lobbyService.GetAllClientLobbies(userId);
        return Ok(lobbies);
    }
    
    /// <summary>
    /// Returns all public lobbies
    /// </summary>
    [HttpGet("allPublicLobbies")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Lobby>> GetAllPubicLobbies()
    {
        var lobbies = _lobbyService.GetAllPublicLobbies();
        return Ok(lobbies);
    }
    
    /// <summary>
    /// Returns all lobbies where user is as guest
    /// </summary>
    [HttpGet("allGuestLobbies")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Lobby>> GetAllGuestLobbies(int userId)
    {
        var lobbies = _lobbyService.GetAllLobbiesWhereClientIsGuest(userId);
        return Ok(lobbies);
    }
    
    /// <summary>
    /// Returns all lobby invites
    /// </summary>
    [HttpGet("allLobbyInvites")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Lobby>> GetAllLobbyInvites(int userId)
    {
        var lobbies = _lobbyService.GetAllLobbiesWhereClientIsInvited(userId);
        return Ok(lobbies);
    }
        
    /// <summary>
    /// Creates new lobby at place
    /// </summary>
    [HttpPost("createLobbyAtPlace")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Lobby> CreateLobbyAtPlace(CreateLobbyAtPlaceRequest request, int userId)
    {
        var lobby = _lobbyService.CreateLobbyAtPlace(request, userId);
        return Ok(lobby);
    }

    /// <summary>
    /// Creates new lobby at custom place
    /// </summary>
    [HttpPost("createLobbyAtCustomPlace")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Lobby> CreateLobbyAtCustomPlace(CreateLobbyAtCustomPlaceRequest request, int userId)
    {
        if (request.CustomPlaceId is null)
        {
            var customPlace = _customPlaceService.CreateCustomPlace(request, userId);
            request.CustomPlaceId = customPlace.Id;
        }
        var lobby = _lobbyService.CreateLobbyAtCustomPlace(request, userId);
        return Ok(lobby);
    }
    
    /// <summary>
    /// Adds guest to lobby
    /// </summary>
    [HttpPut("joinPublicLobby/{userId:int}")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<string> JoinPublicLobby(int userId, int lobbyId)
    {
        var message = _lobbyService.AddGuestToPublicLobby(userId, lobbyId);
        return Ok(message);
    }

    /// <summary>
    /// Adds guest to lobby
    /// </summary>
    [HttpPost("inviteGuest")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<string> InviteGuestToLobby(int userId, int lobbyId)
    {
        var message = _lobbyService.InviteGuestToLobby(userId, lobbyId);
        return Ok(message);
    }

    /// <summary>
    /// Removes guest from lobbyGuests array
    /// </summary>
    [HttpDelete("leaveLobby")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<string> RejectInviteOrLeaveLobby(int userId, int lobbyId)
    {
        var message = _lobbyService.RejectInviteOrLeaveLobby(userId, lobbyId);
        return Ok(message);
    }

    /// <summary>
    /// Accepts invite to the lobby
    /// </summary>
    [HttpPut("acceptInvite/{userId:int}/{lobbyId:int}")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<string> AcceptInviteToLobby(int userId, int lobbyId)
    {
        var message = _lobbyService.AcceptInviteToLobby(userId, lobbyId);
        return Ok(message);
    }
}