using System.Security.Claims;
using GymPortal.Domain;
using GymPortal.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPortal.Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public ActionResult Book(int classId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var exists = _context.Bookings
                .Any(b => b.GymClassId == classId && b.UserId == userId);

            if (exists)
            {
                return RedirectToAction("Index", "Classes");
            }

            var booking = new Booking
            {
                GymClassId = classId,
                UserId = userId!
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("Index", "Classes");
        }

        [Authorize]
        public IActionResult MyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookings = _context.Bookings
                .Where(b => b.UserId == userId)
                .Select(b => b.GymClass)
                .ToList();

            return View(bookings);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Cancel(int classId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var booking = _context.Bookings.FirstOrDefault(b => b.GymClassId == classId && b.UserId == userId);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }

            return RedirectToAction("MyBookings");
        }

    }
}
