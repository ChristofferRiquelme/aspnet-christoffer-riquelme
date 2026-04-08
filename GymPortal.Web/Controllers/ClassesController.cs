using GymPortal.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GymPortal.Web.Controllers
{
    public class ClassesController : Controller
    {
        // GET: ClassesController
        private readonly AppDbContext _context;
        public ClassesController(AppDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var classes = _context.GymClasses.ToList();
            return View(classes);
        }

    }
}
