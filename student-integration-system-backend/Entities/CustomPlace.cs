namespace student_integration_system_backend.Entities;

public class CustomPlace
{
    public int Id { get; set; }
    public string Location { get; set; }
    public string? Description { get; set; }
    public virtual List<Client> Clients { get; set; }
}