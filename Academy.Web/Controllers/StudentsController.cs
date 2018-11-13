﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Academy.Core.Enrollments;
using Academy.Core.Enums;
using Academy.Core.Students;
using Academy.Core.ViewModels;
using Academy.Web.Models;

namespace Academy.Web.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Students
        public async Task<ActionResult> Index(string query = null)
        {
            var students = _context.Students
                .Where(s => s.Status == StudentStatus.Accepted);

            if (!string.IsNullOrWhiteSpace(query))
            {
                students = students.Where(s => s.FirstName.Contains(query) ||
                                               s.SecondName.Contains(query) || s.ThirdName.Contains(query) ||
                                               s.LastName.Contains(query) || s.Mobile1.Contains(query) ||
                                               s.Mobile2.Contains(query)||s.Code.ToString().Contains(query));
            }

            var studentVm = new StudentViewModel()
            {
                Students = await students.ToListAsync(),
                SearchTerm = query
            };

            return View(studentVm);
        }

        [HttpPost]
        public ActionResult Search(StudentViewModel viewModel)
        {
            return RedirectToAction("Index", "Students", new { query = viewModel.SearchTerm });
        }

        [HttpGet, Route("Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {

            var student = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);
            return View(student);
        }

        public async Task<ActionResult> New()
        {
            await GetDropLists();
            var student = new Student();
            return View("StudentForm", student);
        }

        [HttpPost]
        public async Task<ActionResult> Save(Student student)
        {
            if (!ModelState.IsValid)
            {
                await GetDropLists();
                return View("StudentForm", student);
            }
            student.Status = StudentStatus.Accepted;
            student.Code = await GetStudentCide();
            if (student.Id == 0)
                _context.Students.Add(student);
            else
            {
                var studentDb = await _context.Students.SingleAsync(x => x.Id == student.Id);
                TryUpdateModel(studentDb);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Students");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(s => s.Id == id);
            if (student == null) return HttpNotFound();
            await GetDropLists();
            return View("StudentForm", student);
        }

        private async Task GetDropLists()
        {
            ViewBag.Collages = await _context.Collages.ToListAsync();
            ViewBag.Nationalities = await _context.Nationalities.ToListAsync();
            ViewBag.Qualifications = await _context.Qualifiations.ToListAsync();
        }

        public async Task<ActionResult> AssignCourses(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return HttpNotFound();
            var courses = await _context.Courses.ToListAsync();
            var viewModel = new StudentCourseViewModel
            {
                Student = student,
                Courses = courses
            };
            return View("AssignCourseForm", viewModel);
        }


        public async Task<ActionResult> TakeCourse(int id, int courseId)
        {
            var student = await _context.Students.FindAsync(id);
            if (await ValidateStudentcourses(student, courseId))
            {
                _context.Enrollments.Add(new Enrollment
                {
                    StudentId = id,
                    CourseId = courseId
                });
                await _context.SaveChangesAsync();
                TempData["Success"] = "Saved Successfully";
            }
            else
            {
                TempData["Error"] = "there is course for this student in the same time";
                ModelState.AddModelError("", "there is course for this student in the same time");
            }
            var viewModel = new StudentCourseViewModel
            {
                Student = student,
                Courses = await _context.Courses.ToListAsync()
            };
            return View("AssignCourseForm", viewModel);
        }

        private async Task<int> GetStudentCide()
        {
            var code = await _context.Students.MaxAsync(x => x.Code);
            return code == 0 ? 100 : code + 1;
        }
        private async Task<bool> ValidateStudentcourses(Student student, int courseId)
        {
            //var student = await _context.Students.FindAsync(id);
            var course = await _context.Courses.FindAsync(courseId);
            if (student == null || course == null) return true;

            if (student.Enrollments.Any(x => x.Course.TimeFrom == course.TimeFrom &&
                                             x.Course.TimeTo == course.TimeTo))
                return false;

            return true;
        }
    }
}