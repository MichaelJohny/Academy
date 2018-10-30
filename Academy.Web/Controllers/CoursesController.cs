using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Academy.Core.Courses;
using Academy.Core.ViewModels;
using Academy.Web.Models;

namespace Academy.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Courses
        public async Task<ActionResult> Index(string query = null)
        {
            IQueryable<Course> courses = _context.Courses;
            if (!string.IsNullOrWhiteSpace(query))
                courses = courses.Where(c => c.CourseName.Name.Contains(query));
            var courseViewModel = new CourseViewModel()
            {
                Courses = await courses.ToListAsync(),
                SearchTerm = query
            };
            return View(courseViewModel);
        }

        public async Task<ActionResult> New()
        {
            await GetDropLists();
            var course = new Course();
            return View("CourseForm", course);
        }

        private async Task GetDropLists()
        {
            ViewBag.CourseNames = await _context.CourseNames.ToListAsync();     
            ViewBag.Instructors = await _context.Instructors.ToListAsync();     
            ViewBag.CourseLocations = await _context.CourseLocations.ToListAsync();     
        }

        [HttpPost]
        public async Task<ActionResult> Save(Course course)
        {
            if (!ModelState.IsValid)
            {
                await GetDropLists();
                return View("CourseForm", course);
            }
            //before inesert / update -> validate time&Location and instructor
            //if (!ValidateCourse(course))
            //{
            //    throw new Exception("Please Check Course time and instructor");
            //}
            if (course.Id == 0)
                _context.Courses.Add(course);
            else
            {
                var courseDb = await _context.Courses.SingleAsync(x => x.Id == course.Id);
                TryUpdateModel(courseDb);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Courses");
        }

        [HttpPost]
        public ActionResult Search(CourseViewModel course)
        {
            return RedirectToAction("Index", "Courses", new { query = course.SearchTerm });
        }

        public async Task<ActionResult> Edit(int id)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(s => s.Id == id);
            if (course == null) return HttpNotFound();
            await GetDropLists();
            return View("CourseForm", course);
        }

        private bool ValidateCourse(Course course)
        {
            var anyCoursesWithSameTimeAndLocation =
                _context.Courses.Where(c => c.TimeFrom == course.TimeFrom &&
                                            c.CourseLocation.CourseAddress == course.CourseLocation.CourseAddress);
            var diffCoursesWithSameTime =
                course.Instructor.Courses.Where(insCourse => insCourse.TimeFrom == course.TimeFrom);

            if (anyCoursesWithSameTimeAndLocation.Any() && diffCoursesWithSameTime.Any())
                return false;
            else
                return true;

        }
    }
}