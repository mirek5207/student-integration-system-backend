namespace student_integration_system_backend.Entities;

public class LobbyGuest
{
    public int ClientId { get; set; }
    public int LobbyId { get; set; }
    public LobbyGuestStatus Status { get; set; }
    public virtual Lobby Lobby { get; set; }
    public virtual Client Client { get; set; }
}

public enum LobbyGuestStatus
{
    Joined,
    Sent
}