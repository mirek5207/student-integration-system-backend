using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Seeds;
using student_integration_system_backend.Services.FriendService;

namespace student_integration_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FriendController : ControllerBase
{
    private readonly IFriendService _friendService;

    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }
    
    /// <summary>
    /// Returns friendship between two users
    /// </summary>
    [HttpGet("getFriendship")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Friend> GetFriendshipById(int friendshipId)
    {
        var friendship = _friendService.GetFriendshipById(friendshipId);
        return Ok(friendship);
    }
    
    /// <summary>
    /// Returns all user friendships
    /// </summary>
    [HttpGet("getAllFriendships")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Friend>> GetAllClientFriendships(int userId)
    {
        var friendships = _friendService.GetAllClientFriendships(userId);
        return Ok(friendships);
    }
    
    /// <summary>
    /// Returns all invites to friends
    /// </summary>
    [HttpGet("getAllInvites")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Friend>> GetAllClientInvites(int userId)
    {
        var friendships = _friendService.GetAllClientInvites(userId);
        return Ok(friendships);
    }
    
    /// <summary>
    /// Returns all invited users
    /// </summary>
    [HttpGet("getAllInvitedClients")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Friend>> GetAllInvitedClients(int userId)
    {
        var friendships = _friendService.GetAllInvitedClients(userId);
        return Ok(friendships);
    }
    
    /// <summary>
    /// Returns all friends who are not in lobby yet
    /// </summary>
    [HttpGet("getAllFriendsToLobby")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Friend>> GetAllFriendsNotInLobby(int userId, int lobbyId)
    {
        var friendships = _friendService.GetAllFriendsNotInLobby(userId, lobbyId);
        return Ok(friendships);
    }
    
    /// <summary>
    /// Send invite to friends to another user
    /// </summary>
    [HttpPost("sendInvite")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<string> SendInvite(int userId, int friendUserId)
    {
        var message = _friendService.SendInvite(userId, friendUserId);
        return Ok(message);
    }
    
    /// <summary>
    /// Accept invite to friends
    /// </summary>
    [HttpPut("acceptInvite")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<string> AcceptInvite(int friendshipId)
    {
        var message = _friendService.AcceptInvite(friendshipId);
        return Ok(message);
    }
    
    /// <summary>
    /// Reject invite to friends
    /// </summary>
    [HttpDelete("rejectInvite")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<string> RejectInvite(int friendshipId)
    {
        var message = _friendService.RejectInvite(friendshipId);
        return Ok(message);
    }
    
    /// <summary>
    /// Delete user from friend list
    /// </summary>
    [HttpDelete("deleteFriend")]
    [Authorize(Roles = RoleType.Client, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<string> DeleteFriend(int friendshipId)
    {
        var message = _friendService.DeleteFriend(friendshipId);
        return Ok(message);
    }
}