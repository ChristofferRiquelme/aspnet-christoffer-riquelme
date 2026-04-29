using System.Security.Claims;
using GymPortal.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymPortal.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(AppDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        // GET: AccountController
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            return View(user);
        }

        // POST: AccountController
        [HttpPost]
        [Authorize]
        public IActionResult Edit(ApplicationUser model, IFormFile? profileImage)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return NotFound();
            
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumberCustom = model.PhoneNumberCustom;

            if (profileImage != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                var filePath = Path.Combine("wwwroot/images/profiles", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    profileImage.CopyTo(stream);
                }

                user.ProfileImagePath = "/images/profiles/" + fileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return NotFound();
            
            var bookings = _context.Bookings.Where(b => b.UserId == userId);
            _context.Bookings.RemoveRange(bookings);

            var memberships = _context.Memberships.Where(m => m.UserId == userId);
            _context.Memberships.RemoveRange(memberships);
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Your account has been removed.";

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


    }
}
