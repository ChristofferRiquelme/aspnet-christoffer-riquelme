using System;
using GymPortal.Domain;
using GymPortal.Infrastructure;

namespace GymPortal.Web.Models;

public class AccountPageViewModel
{
    public ApplicationUser User { get; set; } = null!;
    public List<Booking> Bookings { get; set; } = new();
    public string ActiveSection { get; set; } = "About";
}
