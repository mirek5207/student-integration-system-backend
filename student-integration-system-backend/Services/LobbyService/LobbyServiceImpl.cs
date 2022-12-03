using Microsoft.EntityFrameworkCore;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Services.ClientService;
using student_integration_system_backend.Services.CustomPlaceService;
using student_integration_system_backend.Services.LobbyGuestService;
using student_integration_system_backend.Services.LobbyOwnerService;
using student_integration_system_backend.Services.PlaceService;

namespace student_integration_system_backend.Services.LobbyService;

public class LobbyServiceImpl : ILobbyService
{
    private readonly AppDbContext _dbContext;
    private readonly IPlaceService _placeService;
    private readonly ICustomPlaceService _customPlaceService;
    private readonly ILobbyOwnerService _lobbyOwnerService;
    private readonly ILobbyGuestService _lobbyGuestService;
    private readonly IClientService _clientService;

    public LobbyServiceImpl(AppDbContext dbContext, IPlaceService placeService, ICustomPlaceService customPlaceService,
        ILobbyOwnerService lobbyOwnerService, ILobbyGuestService lobbyGuestService, IClientService clientService)
    {
        _dbContext = dbContext;
        _placeService = placeService;
        _customPlaceService = customPlaceService;
        _lobbyOwnerService = lobbyOwnerService;
        _lobbyGuestService = lobbyGuestService;
        _clientService = clientService;
    }

    public Lobby CreateLobbyAtPlace(LobbyAtPlaceRequest request, int userId)
    {
        var lobbyOwner = _lobbyOwnerService.GetLobbyOwnerByUserId(userId) ?? _lobbyOwnerService.CreateLobbyOwner(userId);
        var lobby = new Lobby()
        {
            MaxSeats = request.MaxSeats,
            Name = request.Name,
            StartDate = request.StartDate,
            Type = request.Type,
            Place = _placeService.GetPlaceById(request.PlaceId),
            LobbyOwner = lobbyOwner
        };
        _dbContext.Lobbies.Add(lobby);
        _dbContext.SaveChanges();
        return lobby;
    }

    public Lobby CreateLobbyAtCustomPlace(LobbyAtCustomPlaceRequest request, int userId)
    {
        var lobbyOwner = _lobbyOwnerService.GetLobbyOwnerByUserId(userId) ?? _lobbyOwnerService.CreateLobbyOwner(userId);
        var lobby = new Lobby()
        {
            MaxSeats = request.MaxSeats,
            Name = request.Name,
            StartDate = request.StartDate,
            Type = request.Type,
            CustomPlace =  _customPlaceService.GetCustomPlaceById((int) request.CustomPlaceId!),
            LobbyOwner = lobbyOwner
        };
        _dbContext.Lobbies.Add(lobby);
        _dbContext.SaveChanges();
        return lobby;
    }

    public Lobby UpdateLobbyAtPlace(LobbyAtPlaceRequest request, int lobbyId)
    {
        var lobby = GetLobbyById(lobbyId);
        lobby.MaxSeats = request.MaxSeats;
        lobby.Name = request.Name;
        lobby.StartDate = request.StartDate;
        lobby.Type = request.Type;
        lobby.CustomPlace = null;
        lobby.Place = _placeService.GetPlaceById(request.PlaceId);
        _dbContext.SaveChanges();
        return lobby;
    }

    public Lobby UpdateLobbyAtCustomPlace(LobbyAtCustomPlaceRequest request, int lobbyId)
    {
        var lobby = GetLobbyById(lobbyId);
        lobby.MaxSeats = request.MaxSeats;
        lobby.Name = request.Name;
        lobby.StartDate = request.StartDate;
        lobby.Type = request.Type;
        lobby.CustomPlace = _customPlaceService.GetCustomPlaceById((int) request.CustomPlaceId!);
        lobby.Place = null;
        _dbContext.SaveChanges();
        return lobby;
    }

    public Lobby GetLobbyById(int lobbyId)
    {
        var lobby = _dbContext.Lobbies
            .Include(l=>l.CustomPlace)
            .Include(l=>l.LobbyGuests)
            .Include(l=>l.Place)
            .Include(l=>l.LobbyOwner).ThenInclude(lo => lo.Client).IgnoreAutoIncludes()
            .FirstOrDefault(l => l.Id == lobbyId);
        if (lobby is null) throw new NotFoundException("Lobby not found");
        return lobby;
    }

    public string AddGuestToPublicLobby(int userId, int lobbyId)
    {
        var lobby = GetLobbyById(lobbyId);
        if (lobby.Type == LobbyType.Private)
        {
            throw new ForbiddenException("You can't join this lobby!");
        }
        var lobbyGuest = _lobbyGuestService.GetLobbyGuestByUserIdForSpecificLobby(userId, lobbyId) 
                         ?? _lobbyGuestService.CreateLobbyGuest(userId, lobbyId);
        if (lobbyGuest.Status == LobbyGuestStatus.Joined)
        {
            throw new ForbiddenException("You are already in this lobby");
        }
        lobbyGuest.Status = LobbyGuestStatus.Joined;
        _dbContext.SaveChanges();
        
        return "You successfully joined this lobby";
    }

    public string InviteGuestToLobby(int userId, int lobbyId)
    {
        var lobbyGuestExist = _lobbyGuestService.GetLobbyGuestByUserIdForSpecificLobby(userId, lobbyId);
        if (lobbyGuestExist is not null)
        {
            if (lobbyGuestExist.Status == LobbyGuestStatus.Joined)
            {
                throw new ForbiddenException("Lobby guest is already in lobby");
            }

            if (lobbyGuestExist.Status == LobbyGuestStatus.Sent)
            {
                throw new ForbiddenException("Lobby guest is already invited");
            }
        }

        _lobbyGuestService.CreateLobbyGuest(userId, lobbyId);
        return "Invite sent";
    }

    public string RejectInviteOrLeaveLobby(int userId, int lobbyId)
    {
        var lobbyGuest = _lobbyGuestService.GetLobbyGuestByUserIdForSpecificLobby(userId, lobbyId);
        if (lobbyGuest is null)
            throw new NotFoundException("Lobby guest not found");
        var message = (lobbyGuest.Status == LobbyGuestStatus.Joined) ? "Lobby left" : "Invite rejected";
        _dbContext.LobbyGuests.Remove(lobbyGuest);
        _dbContext.SaveChanges();
        return message;
    }

    public string AcceptInviteToLobby(int userId, int lobbyId)
    {
        var lobbyGuest = _lobbyGuestService.GetLobbyGuestByUserIdForSpecificLobby(userId, lobbyId);
        if (lobbyGuest is null)
            throw new NotFoundException("Invite not found");
        if (lobbyGuest.Status == LobbyGuestStatus.Joined)
        {
            throw new ForbiddenException("You are already in this lobby");
        }
        lobbyGuest.Status = LobbyGuestStatus.Joined;
        _dbContext.SaveChanges();
        return "Invite accepted";
    }

    public IEnumerable<Lobby> GetAllLobbies()
    {
        var lobbies = _dbContext.Lobbies.ToList();
        if (lobbies is null) throw new NotFoundException("Lobbies not found");
        return lobbies;
    }

    public IEnumerable<Lobby> GetAllClientLobbies(int userId)
    {
        var lobbies = _dbContext.Lobbies.Include(l => l.LobbyOwner.Client)
            .Where(lo => lo.LobbyOwner.Client.UserId == userId);
        if (lobbies is null) throw new NotFoundException("Lobbies not found");
        return lobbies;
    }

    public IEnumerable<Lobby> GetAllPublicLobbies()
    {
        var lobbies = _dbContext.Lobbies.Where(l => l.Type == LobbyType.Public);
        if (lobbies is null) throw new NotFoundException("Lobbies not found");
        return lobbies;
    }

    public IEnumerable<Lobby> GetAllLobbiesWhereClientIsGuest(int userId)
    {
        var client = _clientService.GetClientByUserId(userId);
        var lobbies = _dbContext.Lobbies.Include(l => l.LobbyGuests)
            .Where(l => (l.LobbyGuests.Where(lg => lg.Client == client && lg.Status == LobbyGuestStatus.Joined)
                .ToList().Count != 0)).ToList();
        if (lobbies is null) throw new NotFoundException("Lobbies not found");
        return lobbies;
    }

    public IEnumerable<Lobby> GetAllLobbiesWhereClientIsInvited(int userId)
    {
        var client = _clientService.GetClientByUserId(userId);
        var lobbies = _dbContext.Lobbies.Include(lobby => lobby.LobbyOwner.Client).ThenInclude(l => l.LobbyGuests)
            .Where(l => (l.LobbyGuests.Where(lg => lg.Client == client && lg.Status == LobbyGuestStatus.Sent)
                .ToList().Count != 0)).ToList();
        if (lobbies is null) throw new NotFoundException("Lobbies not found");
        return lobbies;
    }

    public IEnumerable<LobbyGuest> GetAllLobbyGuestsForLobby(int lobbyId)
    {
        var lobbyGuests = _dbContext.LobbyGuests.Include(lg => lg.Client.User)
            .Where(lg => lg.LobbyId == lobbyId);
        return lobbyGuests;
    }
}