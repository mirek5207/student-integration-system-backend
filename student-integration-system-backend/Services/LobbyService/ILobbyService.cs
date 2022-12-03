using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.LobbyService;

public interface ILobbyService
{
    Lobby CreateLobbyAtPlace(LobbyAtPlaceRequest request, int userId);
    Lobby CreateLobbyAtCustomPlace(LobbyAtCustomPlaceRequest request, int userId);
    Lobby UpdateLobbyAtPlace(LobbyAtPlaceRequest request, int lobbyId);
    Lobby UpdateLobbyAtCustomPlace(LobbyAtCustomPlaceRequest request, int lobbyId);
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