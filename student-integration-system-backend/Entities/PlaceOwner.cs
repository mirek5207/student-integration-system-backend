namespace student_integration_system_backend.Entities;

public class PlaceOwner
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public virtual List<Place> Places { get; set; }
}