using System.Security.Claims;
using GymPortal.Infrastructure;
using GymPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var viewModel = new AccountPageViewModel
            {
                User = user!,
                ActiveSection = "About"
            };

            return View(viewModel);
        }

        // POST: AccountController
        [HttpPost]
        [Authorize]
        public IActionResult Edit(AccountPageViewModel model, IFormFile? profileImage)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return NotFound();
            
            user.FirstName = model.User.FirstName;
            user.LastName = model.User.LastName;
            user.Email = model.User.Email;
            user.PhoneNumberCustom = model.User.PhoneNumberCustom;

            if (profileImage != null && profileImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                profileImage.CopyTo(stream);

                user.ProfileImagePath = "/images/profiles/" + fileName;
            }

            _context.SaveChanges();

            TempData["Message"] = "Your profile has been updated.";

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

        public IActionResult Bookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return NotFound();

            var bookings = _context.Bookings
                .Include(b => b.GymClass)
                .Where(b => b.UserId == userId)
                .ToList();

            var viewModel = new AccountPageViewModel
            {
                User = user,
                Bookings = bookings,
                ActiveSection = "Bookings"
            };

            return View("Index", viewModel);
        }

        public IActionResult Membership()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return NotFound();

            var membership = _context.Memberships
                .FirstOrDefault(m => m.UserId == userId);

            var viewModel = new AccountPageViewModel
            {
                User = user,
                ActiveSection = "Membership"
            };

            if (membership != null)
            {
                viewModel.Membership = membership;
            }

            return View("Index", viewModel);
        }


    }
}
