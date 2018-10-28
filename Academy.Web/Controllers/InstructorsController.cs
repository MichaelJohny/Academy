using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Academy.Core.Instructors;
using Academy.Core.ViewModels;
using Academy.Web.Models;

namespace Academy.Web.Controllers
{
    [Authorize]
    public class InstructorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorsController()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<ActionResult> Index(string query = null)
        {
            var instructors = _context.Instructors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                instructors = instructors.Where(s => s.FirstMidName.Contains(query) ||
                                                     s.LastName.Contains(query) ||
                                                     s.Mobile1.Contains(query) ||
                                                     s.Mobile2.Contains(query));
            }

            var viewModel = new InstructoreViewModel()
            {
                Instructors = await instructors.ToListAsync(),
                SearchTerm = query
            };
            return View("InstructorsView",viewModel);
        }

        [HttpPost]
        public ActionResult Search(InstructoreViewModel viewModel)
        {
            return RedirectToAction("Index", "Instructors", new { query = viewModel.SearchTerm });
        }

        public  ActionResult New()
        {
            //await GetDropLists();
            var student = new Instructor();
            return View("InstructorForm", student);
        }

        [HttpPost]
        public async Task<ActionResult> Save(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return View("InstructorForm", instructor);
            }
            if (instructor.Id == 0)
                _context.Instructors.Add(instructor);
            else
            {
                var instructorDb = await _context.Instructors.SingleAsync(x => x.Id == instructor.Id);
                TryUpdateModel(instructorDb);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Instructors");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var instructor = await _context.Instructors.SingleOrDefaultAsync(s => s.Id == id);
            if (instructor == null) return HttpNotFound();
            
            return View("InstructorForm", instructor);
        }
    }
}