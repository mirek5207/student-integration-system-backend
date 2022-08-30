namespace student_integration_system_backend.Entities;

public class Lobby
{
    public int Id { get; set; }
    public int MaxSeats { get; set; }
    public string Name { get; set; }
    public int? PlaceId { get; set; }
    public int? CustomPlace { get; set; }
    public int LobbyOwnerId { get; set; }
    public virtual LobbyOwner LobbyOwner { get; set; }
    
}