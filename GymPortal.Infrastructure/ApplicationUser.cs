using System;
using Microsoft.AspNetCore.Identity;

namespace GymPortal.Infrastructure;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumberCustom { get; set; }
    public string? ProfileImagePath { get; set; }
}
