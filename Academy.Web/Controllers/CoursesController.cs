using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Academy.Core.ComplexTypes;
using Academy.Core.Courses;
using Academy.Core.Exceptions;
using Academy.Core.ViewModels;
using Academy.Web.Models;

namespace Academy.Web.Controllers
{
    [Authorize]
    public class CoursesController : BaseController
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
            ViewBag.CourseLabs = await _context.CourseLabs.ToListAsync();     
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
            if (!ValidateCourse(course))
            {
                ModelState.AddModelError("", "Please Check Course time and instructor");
                await GetDropLists();
                return View("CourseForm", course);
                //throw new UserFriendlyException("Please Check Course time and instructor");
            }
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

        [HttpPost]
        public async Task<ActionResult> GetCourseLabs(string selectedCourseLocation)
        {
            if (string.IsNullOrEmpty(selectedCourseLocation))
                return Json(new SelectList(new List<BaseComplex>(), "Id", "Name",
                    JsonRequestBehavior.AllowGet));

            var id = Convert.ToInt16(selectedCourseLocation);
            var labs = await _context.CourseLabs.Where(x => x.CourseLocationId == id).ToListAsync();
            return Json(new SelectList(labs, "Id", "Name", JsonRequestBehavior.AllowGet));
        }

        private bool ValidateCourse(Course course)
        {
            var anyCoursesWithSameTimeAndLocation =
                _context.Courses.Where(c => c.TimeFrom == course.TimeFrom && c.DateFrom == course.DateFrom
                                            && c.CourseLocationId == course.CourseLocationId && c.CourseLabId == course.CourseLabId);
            var selectedInstructor = _context.Instructors.SingleOrDefaultAsync(x => x.Id == course.InstructorId);
            IEnumerable<Course> diffCoursesWithSameTime = null;
            if(selectedInstructor?.Result.Courses != null)
                diffCoursesWithSameTime = selectedInstructor?.Result.Courses.Where(insCourse => insCourse.TimeFrom == course.TimeFrom && insCourse.DateFrom == course.DateFrom);

            if (anyCoursesWithSameTimeAndLocation.Any() || diffCoursesWithSameTime.Any())
                //can't add course
                return false;
            else
                //can add course
                return true;
        }

        public async Task<ActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(x => x.Id == id);
            if (course == null) return HttpNotFound();
            course.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Courses");
        }
    }
}