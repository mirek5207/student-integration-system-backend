using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace student_integration_system_backend.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string HashedPassword { get; set; }

    public virtual Account Account { get; set; }
    
    public virtual PlaceOwner PlaceOwner { get; set; }
    
    public virtual List<UserRole> UserRoles { get; set; }
}