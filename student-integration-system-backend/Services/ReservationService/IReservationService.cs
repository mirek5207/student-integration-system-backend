using student_integration_system_backend.Entities;
using student_integration_system_backend.Models.Request;

namespace student_integration_system_backend.Services.ReservationService;

public interface IReservationService
{
    Reservation CreateReservation(CreateReservationRequest request);
}