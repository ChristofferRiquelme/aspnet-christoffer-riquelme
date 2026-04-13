using System;

namespace GymPortal.Domain;

public class Booking
{
    public int Id { get; set; }
    public int GymClassId { get; set; }
    public GymClass GymClass { get; set; } = null!;
    public string UserId { get; set; } = null!;
}
