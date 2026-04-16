using System.Security.Claims;
using GymPortal.Domain;
using GymPortal.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymPortal.Web.Controllers
{
    [Authorize]
    public class MemberShipController : Controller
    {
        private readonly AppDbContext _context;

        public MemberShipController(AppDbContext context)
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
        public ActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var exists = _context.Memberships.Any(m => m.UserId == userId && m.EndDate > DateTime.Now);

            if (!exists)
            {
                var membership = new Membership
                {
                    UserId = userId!,
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
