using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ubuntu_Intelli_Compass.Models;


public class User
{
    [PrimaryKey, AutoIncrement]
    public int UserId { get; set; }

    [NotNull, MaxLength(100)]
    public string FirstName { get; set; } = "";

    [NotNull, MaxLength(100)]
    public string LastName { get; set; } = "";

    [NotNull, MaxLength(100), Unique]
    public string Email { get; set; } = "";

    [NotNull, MaxLength(100)]
    public string PhoneNumber { get; set; }

    [NotNull]
    public DateTime DateOfBirth { get; set; } = DateTime.Today;


    public string? Gender { get; set; }

    [NotNull, MaxLength(100)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}