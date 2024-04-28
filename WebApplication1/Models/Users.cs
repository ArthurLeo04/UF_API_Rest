using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("users")]
    public class Users
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("email")]
        public string Email { get; set; }
        
        [Column("username")]
        public string Username { get; set; }
        
        [Column("password")]
        public string Password { get; set; }
        
        [Column("salt")]
        public string Salt { get; set; }
        
        [Column("kill_count")]
        public int KillCount { get; set; }
        
        [Column("death_count")]
        public int DeathCount { get; set; }
        
        [Column("rank")]
        [DefaultValue("Bronze")]
        public string Rank { get; set; }
        
        [Column("role")]
        [DefaultValue("client")]
        public string Role { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("kd_ratio", TypeName = "numeric")]
        public decimal KDRatio { get; private set; }

    }
}
