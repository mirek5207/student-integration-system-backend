using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Services.ClientService;
using student_integration_system_backend.Services.CustomPlaceService;
using student_integration_system_backend.Services.LobbyOwnerService;
using student_integration_system_backend.Services.PlaceService;

namespace student_integration_system_backend.Services.LobbyService;

public class LobbyServiceImpl : ILobbyService
{
    private readonly AppDbContext _dbContext;
    private readonly IPlaceService _placeService;
    private readonly ICustomPlaceService _customPlaceService;
    private readonly ILobbyOwnerService _lobbyOwnerService;

    public LobbyServiceImpl(AppDbContext dbContext, IPlaceService placeService, ICustomPlaceService customPlaceService,
        ILobbyOwnerService lobbyOwnerService)
    {
        _dbContext = dbContext;
        _placeService = placeService;
        _customPlaceService = customPlaceService;
        _lobbyOwnerService = lobbyOwnerService;
    }


    public Lobby CreateLobby(CreateLobbyRequest request, int userId)
    {
        var lobbyOwner = _lobbyOwnerService.GetLobbyOwnerByUserId(userId) ?? _lobbyOwnerService.CreateLobbyOwner(userId);
        var lobby = new Lobby()
        {
            MaxSeats = request.MaxSeats,
            Name = request.Name,
            Type = request.Type,
            Place = (request.PlaceId is null) ? null : _placeService.GetPlaceById((int) request.PlaceId),
            CustomPlace = (request.CustomPlaceId is null) ?
                null : _customPlaceService.GetCustomPlaceById((int) request.CustomPlaceId),
            LobbyOwner = lobbyOwner
        };
        _dbContext.Lobbies.Add(lobby);
        _dbContext.SaveChanges();
        return lobby;
    }
}