using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("user_achievements")]
    public class UserAchievements
    {
        [Column("id_user")]
        public Guid IdUser { get; set; }

        [Column("id_achievement")]
        public Guid IdAchievement { get; set; }

    }
}
