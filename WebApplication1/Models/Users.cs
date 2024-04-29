using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    [Table("users")]
    public class Users
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("email")]
        [JsonIgnore]
        public string Email { get; set; }
        
        [Column("username")]
        public string Username { get; set; }
        
        [Column("password")]
        [JsonIgnore]
        public string Password { get; set; }
        
        [Column("salt")]
        [JsonIgnore]
        public string Salt { get; set; }
        
        [Column("kill_count")]
        [JsonIgnore]
        public int KillCount { get; set; }
        
        [Column("death_count")]
        [JsonIgnore]
        public int DeathCount { get; set; }
        
        [Column("rank")]
        [DefaultValue("Bronze")]
        public string Rank { get; set; }
        
        [Column("role")]
        [DefaultValue("client")]
        [JsonIgnore]
        public string Role { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("kd_ratio", TypeName = "numeric")]
        [JsonIgnore]
        public decimal KDRatio { get; private set; }

    }
}
