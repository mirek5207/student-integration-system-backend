using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.LobbyService;

public interface ILobbyService
{
    Lobby CreateLobbyAtPlace(CreateLobbyAtPlaceRequest request, int userId);
    Lobby CreateLobbyAtCustomPlace(CreateLobbyAtCustomPlaceRequest request, int userId);
    Lobby GetLobbyById(int lobbyId);
    string AddGuestToPublicLobby(int userId, int lobbyId);
    string InviteGuestToLobby(int userId, int lobbyId);
    string RejectInviteOrLeaveLobby(int userId, int lobbyId);
    string AcceptInviteToLobby(int userId, int lobbyId);
    IEnumerable<Lobby> GetAllLobbies();
    IEnumerable<Lobby> GetAllClientLobbies(int userId);
    IEnumerable<Lobby> GetAllPublicLobbies();
    IEnumerable<Lobby> GetAllLobbiesWhereClientIsGuest(int userId);
    IEnumerable<Lobby> GetAllLobbiesWhereClientIsInvited(int userId);
    IEnumerable<LobbyGuest> GetAllLobbyGuestsForLobby(int lobbyId);
}