using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("friends")]
    public class Friends
    {
        [Column("user1")]
        public Guid User1 { get; set; }

        [Column("user2")]
        public Guid User2 { get; set; }

    }
}
