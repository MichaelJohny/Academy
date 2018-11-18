using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Academy.Core.ComplexTypes;
using Academy.Core.Enums;
using Academy.Core.Students;
using Academy.Core.ViewModels;
using Academy.Web.Models;
using PagedList.EntityFramework;

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
       
        public async Task<ActionResult> Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var applicants = _context.Students.Where(s => s.Status != StudentStatus.Accepted);
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                applicants = applicants.Where(s => s.FirstName.Contains(searchString) ||
                                                   s.SecondName.Contains(searchString) ||
                                                   s.Mobile1.Contains(searchString) ||
                                                   s.Code.ToString().Contains(searchString));
            }
            const int pageSize = 10;
            var pageNumber = (page ?? 1);
            var apps= await applicants.OrderByDescending(x => x.Id).ToPagedListAsync(pageNumber, pageSize);
            return View(apps);
        }
        [HttpPost]
        public ActionResult Search(StudentViewModel viewModel)
        {
            return RedirectToAction("Index", "Applicants", new { query = viewModel.SearchTerm });
        }

        public async Task<ActionResult> New()
        {
            await GetDropLists();
            var student = new Student {BirthDate =null};
            return View("ApplicantForm", student);
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
            student.Code = await GetStudentCide();
            if (student.AreaId == 0)
                student.AreaId = null;
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

        private async Task<int> GetStudentCide()
        {
            if (!await _context.Students.AnyAsync()) return 100;
            var code = await _context.Students.MaxAsync(x => x.Code);
            return code == 0 ? 100 : code + 1;
        }
        public async Task<ActionResult> Edit(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(s => s.Id == id);
            if (student == null) return HttpNotFound();
            await GetDropLists();
            ViewBag.BirthDate = student.BirthDate;
            return View("ApplicantForm", student);
        }

        [HttpPost]
        public async Task<ActionResult> GetAreas(string selectedCity)
        {
            if (string.IsNullOrEmpty(selectedCity))
                return Json(new SelectList(new List<BaseComplex>(), "Id", "Name",
                    JsonRequestBehavior.AllowGet));

            var id = Convert.ToInt16(selectedCity);
            var areas = await _context.Areas.Where(x => x.CityId == id).ToListAsync();
            return Json(new SelectList(areas, "Id", "Name", JsonRequestBehavior.AllowGet));
        }

        private async Task GetDropLists()
        {
            ViewBag.Collages = await _context.Collages.ToListAsync();
            ViewBag.Nationalities = await _context.Nationalities.ToListAsync();
            ViewBag.Qualifications = await _context.Qualifiations.ToListAsync();
            ViewBag.Cities = await _context.Cities.ToListAsync();
            ViewBag.Areas = await _context.Areas.ToListAsync();
            ViewBag.Sepecializations = await _context.Specializations.ToListAsync();
        }
    }
}