using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Intrinsics.Arm;

namespace MyApi.Models
{
    public class User
    {
        [Key]
        public Guid id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string email { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string username { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string password { get; set; }
        
        public string salt { get; set; }
        
        public int kill_count { get; set; }
        
        public int death_count { get; set; }
        
        [ForeignKey("Rank")]
        public string rank { get; set; }

        [ForeignKey("Role")]
        public string role { get; set; }

        public void SetPassword(string password)
        {
            byte[] saltBytes = GenerateSalt();
            string salt = Convert.ToBase64String(saltBytes);

            string combinedPassword = password + salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPassword));
                string hashedPassword = Convert.ToBase64String(hashedBytes);

                this.password = hashedPassword;
                this.salt = salt;
            }
        }

        private byte[] GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return saltBytes;
        }
    }
}
