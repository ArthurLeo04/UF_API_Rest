using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("ranks")]
    public class Ranks
    {
        [Column("rank")]
        public string Rank { get; set; }
    }
}
