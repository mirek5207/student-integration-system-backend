using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Services.ClientService;
using student_integration_system_backend.Services.LobbyService;

namespace student_integration_system_backend.Services.FriendService;

public class FriendServiceImpl : IFriendService
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _clientService;
    private readonly ILobbyService _lobbyService;

    public FriendServiceImpl(AppDbContext dbContext, IClientService clientService, ILobbyService lobbyService)
    {
        _dbContext = dbContext;
        _clientService = clientService;
        _lobbyService = lobbyService;
    }
    
    public void CreateFriendship(int sender, int receiver)
    {
        var friendSender = _clientService.GetClientByUserId(sender);
        var friendReceiver = _clientService.GetClientByUserId(receiver);

        var friendship = new Friend()
        {
            FriendSender = friendSender,
            FriendReceiver = friendReceiver,
            Status = FriendStatus.Invited
        };
        _dbContext.Friends.Add(friendship);
        _dbContext.SaveChanges();
    }

    public bool CheckIfFriendshipExist(int userId, int friendUserId)
    {
        var friendshipExist = GetFriendship(userId, friendUserId) is not null;
        
        return friendshipExist;
    }

    public Friend GetFriendshipById(int friendshipId)
    {
        var friendship = _dbContext.Friends
            .Include(f => f.FriendReceiver)
            .Include(f => f.FriendSender)
            .FirstOrDefault(f => f.Id == friendshipId);
        if (friendship is null)
            throw new NotFoundException("Friendship not found");

        return friendship;
    }

    public Friend? GetFriendship(int userId, int friendUserId)
    {
        var friendship = _dbContext.Friends
            .Include(f => f.FriendSender)
            .Include(f => f.FriendReceiver)
            .FirstOrDefault(fr => (fr.FriendSender.UserId == userId && fr.FriendReceiver.UserId == friendUserId) || 
                       (fr.FriendSender.UserId == friendUserId && fr.FriendReceiver.UserId == userId));
        return friendship;
    }

    public string SendInvite(int userId, int friendUserId)
    {
        if (CheckIfFriendshipExist(userId, friendUserId))
        {
            var friendship = GetFriendship(userId, friendUserId);
            switch (friendship.Status)
            {
                case FriendStatus.Friend:
                    throw new ForbiddenException("You are friends already!");
                case FriendStatus.Invited:
                    throw new ForbiddenException("Invite was sent before. Check received invites.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        CreateFriendship(userId, friendUserId);
        
        return "Invite sent";
    }

    public string AcceptInvite(int friendshipId)
    {
        var friendInvite = GetFriendshipById(friendshipId);
        friendInvite.Status = FriendStatus.Friend;
        _dbContext.SaveChanges();

        return "Invite accepted";
    }

    public string RejectInvite(int friendshipId)
    {
        var friendInvite = GetFriendshipById(friendshipId);
        _dbContext.Friends.Remove(friendInvite);
        _dbContext.SaveChanges();

        return "Invite rejected";
    }

    public string DeleteFriend(int friendshipId)
    {
        var friendship = GetFriendshipById(friendshipId);
        _dbContext.Friends.Remove(friendship);
        _dbContext.SaveChanges();

        return "Friend deleted";
    }

    public IEnumerable<Friend> GetAllClientFriendships(int userId)
    {
        var friendships = _dbContext.Friends
            .Include(f => f.FriendSender)
            .Include(f => f.FriendReceiver)
            .Where(fr => fr.Status == FriendStatus.Friend &&
                         (fr.FriendSender.UserId == userId || fr.FriendReceiver.UserId == userId)).ToList();
        if (friendships.Count == 0)
        {
            throw new NotFoundException("Friendships not found");
        }

        return friendships;
    }

    public IEnumerable<Friend> GetAllClientInvites(int userId)
    {
        var friendships = _dbContext.Friends
            .Include(f => f.FriendSender)
            .Include(f => f.FriendReceiver)
            .Where(fr => fr.Status == FriendStatus.Invited && fr.FriendReceiver.UserId == userId).ToList();
        if (friendships is null)
        {
            throw new NotFoundException("Friendships not found");
        }

        return friendships;
    }

    public IEnumerable<Friend> GetAllInvitedClients(int userId)
    {
        var friendships = _dbContext.Friends
            .Include(f => f.FriendSender)
            .Include(f => f.FriendReceiver)
            .Where(fr => fr.Status == FriendStatus.Invited && fr.FriendSender.UserId == userId).ToList();
        if (friendships is null)
        {
            throw new NotFoundException("Friendships not found");
        }

        return friendships;
    }

    public IEnumerable<Friend> GetAllFriendsNotInLobby(int userId, int lobbyId)
    {
        var clientLobbyGuests = _lobbyService.GetAllLobbyGuestsForLobby(lobbyId).Select(lg => lg.Client).ToList();
        var friendsWithoutLobbyGuests = GetAllClientFriendships(userId).Where(fr =>
            clientLobbyGuests.Any(lg => fr.FriendSenderId != lg.Id && fr.FriendReceiverId != lg.Id)).ToList();
            
        return friendsWithoutLobbyGuests;
    }
}