namespace student_integration_system_backend.Entities;

public class Lobby
{
    public int Id { get; set; }
    public int MaxSeats { get; set; }
    public string Name { get; set; }
    public LobbyType Type { get; set; }
    public int? PlaceId { get; set; }
    public int? CustomPlaceId { get; set; }
    public int LobbyOwnerId { get; set; }
    public virtual LobbyOwner LobbyOwner { get; set; }
    public virtual Place? Place { get; set; }
    public virtual CustomPlace? CustomPlace { get; set; }
    public virtual List<LobbyGuest> LobbyGuests { get; set; }
}

public enum LobbyType
{
    Public,
    Private
}