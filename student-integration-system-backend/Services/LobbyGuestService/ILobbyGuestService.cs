using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Services.LobbyGuestService;

public interface ILobbyGuestService
{
    LobbyGuest CreateLobbyGuest(int userId, int lobbyId);
    LobbyGuest? GetLobbyGuestByUserIdForSpecificLobby(int userId, int lobbyId);
    
}