﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Academy.Core.ComplexTypes;
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
            GetDays();
            await GetDropLists();
            var course = new Course { DateFrom = DateTime.Now, DateTo = DateTime.Now };

            return View("CourseForm", course);
        }

        private async Task GetDropLists()
        {
            ViewBag.CourseNames = await _context.CourseNames.ToListAsync();
            ViewBag.Instructors = await _context.Instructors.ToListAsync();
            ViewBag.CourseLocations = await _context.CourseLocations.ToListAsync();
            ViewBag.CourseLabs = await _context.CourseLabs.ToListAsync();
            ViewBag.Users = await _context.Users.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Batches = await _context.Batches.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Save(Course course)
        {
            if (!ModelState.IsValid)
            {
                GetDays();
                await GetDropLists();
                return View("CourseForm", course);
            }
            //before inesert / update -> validate time&Location and instructor
            if (course.Id == 0 && !await ValidateCourse(course))
            {
                ModelState.AddModelError("", "Please Check Course time and instructor");
                GetDays();
                await GetDropLists();
                return View("CourseForm", course);
            }
            if (!ValidateCourseGroupNumber(course))
            {
                ModelState.AddModelError("", "Can't Insert duplicated Group Number");
                GetDays();
                await GetDropLists();
                return View("CourseForm", course);
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
            GetDays();
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

        private async Task<bool> ValidateCourse(Course course)
        {
            var anyCoursesWithSameTimeAndLocation =
                _context.Courses.Where(c => c.CourseLocationId == course.CourseLocationId &&
                                            c.CourseLabId == course.CourseLabId &&
                                            c.TimeFrom <= course.TimeFrom && c.TimeTo >= course.TimeFrom
                                            && DbFunctions.TruncateTime(c.DateFrom) <=
                                            DbFunctions.TruncateTime(course.DateFrom) &&
                                            DbFunctions.TruncateTime(c.DateTo) >=
                                            DbFunctions.TruncateTime(course.DateFrom)
                );
            var selectedInstructor = await _context.Instructors.SingleOrDefaultAsync(x => x.Id == course.InstructorId);
            IEnumerable<Course> diffCoursesWithSameTime = null;
            if (selectedInstructor?.Courses != null)
                diffCoursesWithSameTime =
                    selectedInstructor.Courses.Where(insCourse => insCourse.TimeFrom <= course.TimeFrom &&
                                                                  insCourse.TimeTo >= course.TimeFrom &&
                                                                  DbFunctions.TruncateTime(insCourse.DateFrom) <=
                                                                  DbFunctions.TruncateTime(course.DateFrom) &&
                                                                  DbFunctions.TruncateTime(insCourse.DateTo) >=
                                                                  DbFunctions.TruncateTime(course.DateFrom));

            return !anyCoursesWithSameTimeAndLocation.Any() && !diffCoursesWithSameTime.Any();
        }

        private bool ValidateCourseGroupNumber(Course course)
        {
            var allCoursesWithSameLocationAndCategory =
                _context.Courses.Where(c => c.CourseLocationId == course.CourseLocationId &&
                                            c.Batch.CategoryId == course.Batch.CategoryId);
            if (!allCoursesWithSameLocationAndCategory.Any()) return true;
            var allGroupNumbers = allCoursesWithSameLocationAndCategory.Select(pr => pr.GroupNumber);
            return !allGroupNumbers.Contains(course.GroupNumber);
        }

        public async Task<ActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(x => x.Id == id);
            if (course == null) return HttpNotFound();
            course.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Courses");
        }

        private void GetDays()
        {
            var course = new Course();
            ViewBag.Days = course.GetDays()
                .Select(day => new SelectListItem
                {
                    Value = day,
                    Text = day
                })
                .ToList();

        }

    }
}