using student_integration_system_backend.Entities;
using student_integration_system_backend.Exceptions;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Services.LobbyService;
using student_integration_system_backend.Services.PlaceService;

namespace student_integration_system_backend.Services.ReservationService;

public class ReservationServiceImpl : IReservationService
{
    private readonly IPlaceService _placeService;
    private readonly ILobbyService _lobbyService;
    private readonly AppDbContext _dbContext;

    public ReservationServiceImpl(IPlaceService placeService, ILobbyService lobbyService, AppDbContext dbContext)
    {
        _placeService = placeService;
        _lobbyService = lobbyService;
        _dbContext = dbContext;
    }
    
    public Reservation CreateReservation(CreateReservationRequest request)
    {
        var reservation = new Reservation()
        {
            NumberOfGuests = request.NumberOfGuests,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PhoneNumber = request.PhoneNumber,
            Status = ReservationStatus.Sent,
            Place = _placeService.GetPlaceById(request.PlaceId),
            Lobby = _lobbyService.GetLobbyById(request.LobbyId)
        };
        _dbContext.Reservations.Add(reservation);
        _dbContext.SaveChanges();
        return reservation;
    }

    public IEnumerable<Reservation> GetAllConfirmedReservationsForSpecificdLobbyAndDay(DateTime date, int placeId)
    {
        var reservations = _dbContext.Reservations.Where(r => r.Status == ReservationStatus.Confirmed
                                                              && (r.StartDate.Date == date.Date ||
                                                                  r.EndDate.Date == date.Date)
                                                              && r.PlaceId == placeId).ToList();
        if (reservations.Count == 0)
            throw new NotFoundException("Reservations for this day not found.");
        return reservations;
    }
}