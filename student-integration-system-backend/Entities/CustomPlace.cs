namespace student_integration_system_backend.Entities;

public class CustomPlace
{
    public int Id { get; set; }
    public string Location { get; set; }
    public string? Description { get; set; }
    public int ClientId { get; set; }
    public virtual Client Client { get; set; }
}