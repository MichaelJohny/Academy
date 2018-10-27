using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Academy.Core.Enums;
using Academy.Web.Models;

namespace Academy.Web.Controllers
{
    public class ApplicantsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ApplicantsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Applicants
        public ActionResult Index()
        {
            var applicants = _context.Students.Where(s => s.Status != StudentStatus.Accepted);
            return View(applicants);
        }

        public async Task<ActionResult> UpdateStatus(int id,StudentStatus status)
        {

            var student =await _context.Students.SingleOrDefaultAsync(x=>x.Id==id);
            if (student == null) return HttpNotFound();
            student.Status = status;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Applicants");
        }
    }
}