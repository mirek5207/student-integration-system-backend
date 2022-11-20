using System.Linq;
using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Services.ClientService;

namespace student_integration_system_backend.Services.LobbyOwnerService;

public class LobbyOwnerServiceImpl : ILobbyOwnerService
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _clientService;
    
    public LobbyOwnerServiceImpl(AppDbContext dbContext, IClientService clientService)
    {
        _dbContext = dbContext;
        _clientService = clientService;
    }
    
    public LobbyOwner? GetLobbyOwnerByUserId(int userId)
    {
        var lobbyOwner = _dbContext.LobbyOwners.Include(owner => owner.Client)
            .FirstOrDefault(owner => owner.Client.UserId == userId);
        return lobbyOwner;
    }

    public LobbyOwner CreateLobbyOwner(int userId)
    {
        var lobbyOwner = new LobbyOwner()
        {
            Client = _clientService.GetClientByUserId(userId)
        };
        _dbContext.LobbyOwners.Add(lobbyOwner);
        _dbContext.SaveChanges();
        return lobbyOwner;
    }
}