namespace student_integration_system_backend.Entities;

public class Moderator
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
}