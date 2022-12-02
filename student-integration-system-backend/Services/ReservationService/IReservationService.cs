using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Models.Response;

namespace student_integration_system_backend.Services.ReservationService;

public interface IReservationService
{
    Reservation CreateReservation(CreateReservationRequest request);
    IEnumerable<ReservationResponse> GetAllConfirmedReservationsForSpecificPlaceAndDay(DateTime day, int placeId);
    IEnumerable<ReservationResponse> GetAllSentReservationsForPlace(int placeId);
    Reservation GetReservationById(int reservationId);
    string DeclinedReservation(int reservationId);
    string ConfirmReservation(int reservationId);
    string DeleteReservation(int reservationId);
    Reservation UpdateReservation(UpdateReservationRequest request, int reservationId);

}