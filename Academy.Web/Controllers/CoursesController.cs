using System.Web.Mvc;
using Academy.Web.Models;

namespace Academy.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Courses
        public ActionResult Index()
        {
            return View();
        }


    }
}