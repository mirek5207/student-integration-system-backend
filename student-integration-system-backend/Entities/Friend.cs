using System.ComponentModel.DataAnnotations.Schema;

namespace student_integration_system_backend.Entities;

public class Friend
{
    public int Id { get; set; }
    public int FriendId { get; set; }
    public int UserId { get; set; }
    
    [ForeignKey("FriendId")]
    public virtual User FriendUser { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    
    

}