using System.ComponentModel.DataAnnotations.Schema;

namespace student_integration_system_backend.Entities;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")] public virtual User User { get; set; }
    public virtual List<CustomPlace> CustomPlaces { get; set; }
    public virtual List<LobbyOwner> LobbyOwners { get; set; }
    public virtual List<LobbyGuest> LobbyGuests { get; set; }
}