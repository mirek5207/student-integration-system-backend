namespace student_integration_system_backend.Entities;

public class LobbyOwner
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public virtual Client Client { get; set; }
}