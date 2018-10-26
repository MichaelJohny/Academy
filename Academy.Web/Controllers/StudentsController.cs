using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
            var students = _context.Students;
            return View(students);
        }

        public async Task<ActionResult> Details(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(x=>x.Id==id);
            return View(student);
        }

        public ActionResult New()
        {
            var student = new Student();
            return View(student);
        }
    }
}