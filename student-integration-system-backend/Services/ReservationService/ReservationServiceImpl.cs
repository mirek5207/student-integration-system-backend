using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Services.LobbyService;
using student_integration_system_backend.Services.PlaceService;

namespace student_integration_system_backend.Services.ReservationService;

public class ReservationServiceImpl : IReservationService
{
    private readonly IPlaceService _placeService;
    private readonly ILobbyService _lobbyService;
    private readonly AppDbContext _appDbContext;

    public ReservationServiceImpl(IPlaceService placeService, ILobbyService lobbyService, AppDbContext appDbContext)
    {
        _placeService = placeService;
        _lobbyService = lobbyService;
        _appDbContext = appDbContext;
    }
    
    public Reservation CreateReservation(CreateReservationRequest request)
    {
        var reservation = new Reservation()
        {
            NumberOfGuests = request.NumberOfGuests,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = ReservationStatus.Sent,
            Place = _placeService.GetPlaceById(request.PlaceId),
            Lobby = _lobbyService.GetLobbyById(request.LobbyId)
        };
        _appDbContext.Reservations.Add(reservation);
        _appDbContext.SaveChanges();
        return reservation;
    }
}