using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Services.LobbyOwnerService;

public interface ILobbyOwnerService
{
    LobbyOwner? GetLobbyOwnerByUserId(int lobbyOwnerId);
    LobbyOwner CreateLobbyOwner(int userId);
}