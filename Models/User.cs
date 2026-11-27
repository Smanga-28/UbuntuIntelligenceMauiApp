using SQLite;

namespace Ubuntu_Intelli_Compass.Models;


[Table("USERS")]
public class User
{
    [PrimaryKey, AutoIncrement, Column("user_id")]
    public int UserId { get; set; }

    [Column("first_name"), NotNull, MaxLength(100)]
    public string FirstName { get; set; } = "";

    [Column("last_name"), NotNull, MaxLength(100)]
    public string LastName { get; set; } = "";

    [Column("email"), NotNull, MaxLength(100), Unique]
    public string Email { get; set; } = "";

    [Column("phone_number"), MaxLength(100)]
    public string PhoneNumber { get; set; } = "";

    [Column("date_of_birth")]
    public DateTime DateOfBirth { get; set; } = DateTime.Today;

    [Column("gender")]
    public string? Gender { get; set; }

    [Column("created_at"), NotNull]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
