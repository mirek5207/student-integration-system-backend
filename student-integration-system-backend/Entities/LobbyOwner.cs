namespace student_integration_system_backend.Entities;

public class LobbyOwner
{
    public int Id { get; set; }
    public virtual List<Client> Clients { get; set; }
}