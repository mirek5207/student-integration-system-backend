namespace student_integration_system_backend.Entities;

public class Friend
{
    public int Id { get; set; }
    public virtual List<Friend> Friends { get; set; }
}