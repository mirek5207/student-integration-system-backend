namespace student_integration_system_backend.Entities;

public class Account
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime AccountCreationTime { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
}