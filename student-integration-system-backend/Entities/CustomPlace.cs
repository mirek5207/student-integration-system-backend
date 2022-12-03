namespace student_integration_system_backend.Entities;

public class CustomPlace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Description { get; set; }
    public int ClientId { get; set; }
    public virtual Client Client { get; set; }
}