using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.LobbyService;

public interface ILobbyService
{
    Lobby CreateLobby(CreateLobbyRequest request, int userId);
}