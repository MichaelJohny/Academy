using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Academy.Core.Enums;
using Academy.Core.Students;
using Academy.Core.ViewModels;
using Academy.Web.Models;

namespace Academy.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController()
        {
            _context = new ApplicationDbContext();
        }

        
        // GET: Students
        public  ActionResult Index()
        {
            var students = _context.Students.Where(s=>s.Status==StudentStatus.Accepted);
            return View(students);
        }

        [HttpGet,Route("Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            
            var student = await _context.Students.SingleOrDefaultAsync(x=>x.Id==id);
            return View(student);
        }

        public ActionResult New()
        {
            var student = new Student();
            return View("StudentForm",student);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
            //change to save
            //heck model state
            //add update
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Students");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var customer =await _context.Students.SingleOrDefaultAsync(s => s.Id == id);
            if (customer == null) return HttpNotFound();
            return View("StudentForm", customer);
        }
    }
}