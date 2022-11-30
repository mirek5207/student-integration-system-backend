namespace student_integration_system_backend.Entities;

public class Reservation
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfGuests { get; set; }
    public ReservationStatus Status { get; set; }
    public string PhoneNumber { get; set; }
    public int PlaceId { get; set; }
    public int LobbyId { get; set; }
    public virtual Place Place { get; set; }
    public virtual Lobby Lobby { get; set; }
}

public enum ReservationStatus
{
    Sent,
    Confirmed,
    Declined
}