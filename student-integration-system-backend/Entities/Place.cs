namespace student_integration_system_backend.Entities;

public class Place
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public virtual List<PlaceOwner> PlaceOwners { get; set; }
}