using System;

namespace GymPortal.Domain;

public class Membership
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public bool IsActive => EndDate > DateTime.Now;
}
