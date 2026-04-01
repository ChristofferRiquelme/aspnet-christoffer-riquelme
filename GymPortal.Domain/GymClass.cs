using System;

namespace GymPortal.Domain;

public class GymClass
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Instructor { get; set; } = null!;
    public string Category { get; set; } = null!;
}
