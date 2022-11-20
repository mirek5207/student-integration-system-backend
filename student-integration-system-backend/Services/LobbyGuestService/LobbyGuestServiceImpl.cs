using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Services.ClientService;
using student_integration_system_backend.Services.LobbyService;

namespace student_integration_system_backend.Services.LobbyGuestService;

public class LobbyGuestServiceImpl : ILobbyGuestService
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _clientService;

    
    public LobbyGuestServiceImpl(AppDbContext dbContext, IClientService clientService)
    {
        _dbContext = dbContext;
        _clientService = clientService;
    }
    
    public LobbyGuest CreateLobbyGuest(int userId, int lobbyId)
    {
        var lobby = _dbContext.Lobbies
            .Include(l=>l.CustomPlace)
            .Include(l=>l.LobbyGuests)
            .Include(l=>l.Place)
            .Include(l=>l.LobbyOwner).ThenInclude(lo => lo.Client).IgnoreAutoIncludes()
            .FirstOrDefault(l => l.Id == lobbyId);
        if (lobby is null) throw new NotFoundException("Lobby not found");
        
        var lobbyGuest = new LobbyGuest()
        {
            Client = _clientService.GetClientByUserId(userId),
            Lobby = lobby,
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