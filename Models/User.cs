using System;
using System.Collections.Generic;
using System.Text;

namespace Ubuntu_Intelli_Compass.Models;

public sealed class User
{
    public long UserId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string PreferredLanguage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string AccountStatus { get; set; }
}