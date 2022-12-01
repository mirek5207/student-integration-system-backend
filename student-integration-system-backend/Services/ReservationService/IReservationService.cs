using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.ReservationService;

public interface IReservationService
{
    Reservation CreateReservation(CreateReservationRequest request);
    IEnumerable<Reservation> GetAllConfirmedReservationsForSpecificPlaceAndDay(DateTime day, int lobbyId);
    IEnumerable<Reservation> GetAllSentReservationsForPlace(int placeId);
    Reservation GetReservationById(int reservationId);
    string DeclinedReservation(int reservationId);
    string ConfirmReservation(int reservationId);
    string DeleteReservation(int reservationId);
}