using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Services.FriendService;

public interface IFriendService
{
    void CreateFriendship(int sender, int receiver);
    bool CheckIfFriendshipExist(int userId, int friendUserId);
    Friend GetFriendshipById(int friendshipId);
    Friend? GetFriendship(int userId, int friendUserId);
    string SendInvite(int userId, int friendUserId);
    string AcceptInvite(int friendshipId);
    string RejectInvite(int friendshipId);
    string DeleteFriend(int friendshipId);
    IEnumerable<Friend> GetAllClientFriendships(int userId);
    IEnumerable<Friend> GetAllClientInvites(int userId);
    IEnumerable<Friend> GetAllInvitedClients(int userId);
    IEnumerable<Client> GetAllFriendsNotInLobby(int userId, int lobbyId);
}