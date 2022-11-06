using System.ComponentModel.DataAnnotations;

namespace student_integration_system_backend.Entities;

public class Place
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int PlaceOwnerId { get; set; }
    public virtual PlaceOwner PlaceOwner { get; set; }
}