using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("achievements")]
    public class Achievements
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("nom")]
        public string Nom { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("image")]
        public string Image { get; set; }
    }
}
