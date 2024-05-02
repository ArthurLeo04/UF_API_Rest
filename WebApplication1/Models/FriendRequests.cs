using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("friend_requests")]
    public class FriendRequests
    {
        [Column("sender")]
        public Guid Sender { get; set; }

        [Column("receiver")]
        public Guid Receiver { get; set; }

    }
}
