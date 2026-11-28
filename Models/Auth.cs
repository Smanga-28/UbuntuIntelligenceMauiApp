
using SQLite;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace Ubuntu_Intelli_Compass.Models
{
    [Table("AUTH_CREDENTIALS")]
    public class Auth
    {
        [Column("credential_id"), PrimaryKey, AutoIncrement]
        public int CredentialId { get; set; }

        [Column("user_id"), Unique, NotNull]
        public int UserId { get; set; }

        [Column("auth_type"), NotNull]
        public string AuthType { get; set; } = "";

        [Column("secret_hash")]
        public string SecretHash { get; set; } = "";

        [Column("created_at"), NotNull, Timestamp]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("last_used_at"), NotNull, Timestamp]
        public DateTime LastUsedAt { get; set; } = DateTime.UtcNow;





    }
}
