using System;

namespace GymPortal.Domain;

public class Membership

{

    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Type { get; set; } = "Standard"; // Standard / Premium

    public decimal Price { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive => EndDate > DateTime.Now;

}
