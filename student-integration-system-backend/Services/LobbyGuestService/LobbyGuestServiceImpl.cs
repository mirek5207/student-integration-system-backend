using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Services.ClientService;
using student_integration_system_backend.Services.LobbyService;

namespace student_integration_system_backend.Services.LobbyGuestService;

public class LobbyGuestServiceImpl : ILobbyGuestService
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _clientService;
    private readonly ILobbyService _lobbyService;
    
    public LobbyGuestServiceImpl(AppDbContext dbContext, IClientService clientService, ILobbyService lobbyService)
    {
        _dbContext = dbContext;
        _clientService = clientService;
        _lobbyService = lobbyService;
    }
    
    public LobbyGuest CreateLobbyGuest(int userId, int lobbyId)
    {
        var lobbyGuest = new LobbyGuest()
        {
            Client = _clientService.GetClientByUserId(userId),
            Lobby = _lobbyService.GetLobbyById(lobbyId),
            Status = LobbyGuestStatus.Sent
        };
        _dbContext.LobbyGuests.Add(lobbyGuest);
        _dbContext.SaveChanges();
        return lobbyGuest;
    }

    public LobbyGuest? GetLobbyGuestByUserIdForSpecificLobby(int userId, int lobbyId)
    {
        var lobbyGuest = _dbContext.LobbyGuests.Include(lg => lg.Client)
            .FirstOrDefault(lg => lg.Client.UserId == userId && lg.LobbyId == lobbyId);
        return lobbyGuest;
    }
}