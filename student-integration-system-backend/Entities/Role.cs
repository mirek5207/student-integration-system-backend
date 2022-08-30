namespace student_integration_system_backend.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual List<UserRole> UserRoles { get; set; }

}