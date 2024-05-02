using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("roles")]
    public class Roles
    {
        [Column("role")]
        public string Role { get; set; }
    }
}
