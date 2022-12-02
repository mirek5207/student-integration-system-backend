using student_integration_system_backend.Entities;

namespace student_integration_system_backend.Models.Response;

public class ReservationResponse
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfGuests { get; set; }
    public ReservationStatus Status { get; set; }
    public string PhoneNumber { get; set; }
    public string LobbyOwnerFullName { get; set; }
}