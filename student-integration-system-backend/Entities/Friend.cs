using System.ComponentModel.DataAnnotations.Schema;

namespace student_integration_system_backend.Entities;

public class Friend
{
    public int Id { get; set; }
    public int FriendSenderId { get; set; }
    public int FriendReceiverId { get; set; }
    public FriendStatus Status { get; set; }
    [ForeignKey("FriendSenderId")]
    public virtual Client FriendSender { get; set; }
    [ForeignKey("FriendReceiverId")]
    public virtual Client FriendReceiver { get; set; }
}

public enum FriendStatus
{
    Friend,
    Invited
}