using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace student_integration_system_backend.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int RoleId { get; set; }
    public virtual Role Role { get; set; }
    public virtual Account Account { get; set; }
}