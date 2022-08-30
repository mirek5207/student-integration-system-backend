namespace student_integration_system_backend.Entities;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int FriendId { get; set; }
    public virtual Friend Friend { get; set; }
    public int CustomPlaceId { get; set; }
    public virtual CustomPlace CustomPlace { get; set; }
    public int LobbyOwnerId { get; set; }
    public virtual LobbyOwner LobbyOwner { get; set; }
    public int LobbyGuestId { get; set; }
    public virtual LobbyGuest LobbyGuest { get; set; }
}