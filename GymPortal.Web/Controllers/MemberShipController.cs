using System.Security.Claims;
using GymPortal.Domain;
using GymPortal.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPortal.Web.Controllers
{
    [Authorize]
    public class MembershipController : Controller
    {
        private readonly AppDbContext _context;

        public MembershipController(AppDbContext context)
        {
            _context = context;
        }
        // GET: MemberShipController
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var membership = _context.Memberships.FirstOrDefault(m => m.UserId == userId);

            return View(membership);
        }

        [HttpPost]
        public ActionResult Create(string type)

        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var exists = _context.Memberships.Any(m => m.UserId == userId && m.EndDate > DateTime.Now);

            if (!exists)

            {
                var price = type == "Premium" ? 595.00m : 495.00m;

                var membership = new Membership
                {

                    UserId = userId!,

                    Type = type,

                    Price = price,

                    StartDate = DateTime.Now,

                    EndDate = DateTime.Now.AddMonths(1)
                };

                _context.Memberships.Add(membership);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");

        }
    }
}
