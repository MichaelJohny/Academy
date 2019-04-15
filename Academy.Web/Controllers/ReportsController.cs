using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Academy.Core.ComplexTypes;
using Academy.Core.Dtos;
using Academy.Web.Models;

namespace Academy.Web.Controllers
{
    [RoutePrefix("reports")]
    public class ReportsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ReportsController()
        {
            _context = new ApplicationDbContext();
        }
        

        [Route("")]
        public ActionResult Reporst()
        {
            return View();
        }

        //public async Task<ActionResult> GetDate(SearchInput input)
        //{
        //    var data = Context.Courses.Where(x =>
        //        DbFunctions.TruncateTime(input.StartDate) >= DbFunctions.TruncateTime(x.DateFrom)
        //        && DbFunctions.TruncateTime(input.EndDate) <= DbFunctions.TruncateTime(x.DateTo));
        //    foreach (var course in data)
        //    {
        //        var students= course.Enrollments.Select(x => x.Student);

        //    }


        //    //var students = Context.Students.Where(x =>
        //    //        DbFunctions.TruncateTime(input.StartDate) >= DbFunctions.TruncateTime(x.CreationTime)
        //    //        && DbFunctions.TruncateTime(input.EndDate) <= DbFunctions.TruncateTime(x.CreationTime))
        //    //    .FirstOrDefault();
        //    //students.Enrollments.Where(s=>s.CourseId)
        //}

        [Route("groupNumbers")]
        public async Task<ActionResult> GetGroupNumbers()
        {
            var result = await _context.Courses.Select(x => x.GroupNumber).ToListAsync();
            return AjaxResponse(true, result);
        }

        [Route("batches")]
        public async Task<ActionResult> GetBatches()
        {
            var result = await _context.Batches
                .Select(x => new BaseComplex() {Id = x.Id, Name = x.BatchNumber.ToString()}).ToListAsync();
            return AjaxResponse(true, result);
        }
    }
}