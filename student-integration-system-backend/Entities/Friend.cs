using System.ComponentModel.DataAnnotations.Schema;

namespace student_integration_system_backend.Entities;

public class Friend
{
    public int Id { get; set; }
    public int FriendOneId { get; set; }
    public int FriendTwoId { get; set; }
    public FriendStatus Status { get; set; }
    [ForeignKey("FriendOneId")]
    public virtual Client FriendOn { get; set; }
    [ForeignKey("FriendTwoId")]
    public virtual Client FriendTwo { get; set; }
}

public enum FriendStatus
{
    Friend,
    Invited
}