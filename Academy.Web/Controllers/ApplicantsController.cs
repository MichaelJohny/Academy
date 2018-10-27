using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Academy.Core.Enums;
using Academy.Core.Students;
using Academy.Core.ViewModels;
using Academy.Web.Models;

namespace Academy.Web.Controllers
{
    [Authorize]
    public class ApplicantsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ApplicantsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Applicants
        public async Task<ActionResult> Index(string query = null)
        {
            var applicants =  _context.Students.Where(s => s.Status != StudentStatus.Accepted);
            if (!string.IsNullOrWhiteSpace(query))
            {
                applicants = applicants.Where(s => s.FirstName.Contains(query) ||
                                               s.SecondName.Contains(query) || s.ThirdName.Contains(query) ||
                                               s.LastName.Contains(query) || s.Mobile1.Contains(query) ||
                                               s.Mobile2.Contains(query));
            }
            var studentVm = new StudentViewModel()
            {
                Students = await applicants.ToListAsync(),
                SearchTerm = query
            };

            return View(studentVm);
        }
        [HttpPost]
        public ActionResult Search(StudentViewModel viewModel)
        {
            return RedirectToAction("Index", "Applicants", new { query = viewModel.SearchTerm });
        }

        public async Task<ActionResult> New()
        {
            await GetDropLists();
            var student = new Student();
            return View("ApplicantForm",student);
        }

        public async Task<ActionResult> UpdateStatus(int id, StudentStatus status)
        {

            var student = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);
            if (student == null) return HttpNotFound();
            student.Status = status;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Applicants");
        }



        [HttpPost]
        public async Task<ActionResult> Save(Student student)
        {
            if (!ModelState.IsValid)
            {
                await GetDropLists();
                return View("ApplicantForm", student);
            }
                
            student.Status = StudentStatus.Pending;
            if (student.Id == 0)
                _context.Students.Add(student);
            else
            {
                var studentDb = await _context.Students.SingleAsync(x => x.Id == student.Id);
                TryUpdateModel(studentDb);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Applicants");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(s => s.Id == id);
            if (student == null) return HttpNotFound();
            ViewBag.Collages = await _context.Collages.ToListAsync();
            ViewBag.Nationalities = await _context.Nationalities.ToListAsync();
            ViewBag.Qualifications = await _context.Qualifiations.ToListAsync();
            return View("ApplicantForm", student);
        }

        private async Task GetDropLists()
        {
            ViewBag.Collages = await _context.Collages.ToListAsync();
            ViewBag.Nationalities = await _context.Nationalities.ToListAsync();
            ViewBag.Qualifications = await _context.Qualifiations.ToListAsync();
        }
    }
}