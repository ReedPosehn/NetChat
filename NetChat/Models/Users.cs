using System.ComponentModel.DataAnnotations.Schema;

namespace NetChat.Models
{
    public class User
    {
        [Column("USER_ID")]
        public int UserId { get; set; } // Primary key
        public string Username { get; set; } // Unique username
        [Column("PASS_HASH")]
        public string PasswordHash { get; set; } // Hashed password.
    }
}