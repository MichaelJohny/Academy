using System.Text;
using System.Web.Mvc;
using Academy.Core.Exceptions;
using Academy.Core.Extensions;
using Academy.Web.Helpers;

namespace Academy.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext context)
        {
            if (context.Exception is UserFriendlyException)
            {
                context.ExceptionHandled = true;
                context.Result = Content(new AjaxResponse
                {
                    Success = false,
                    Result = context.Exception.Message
                }.ToJson(), "application/json", Encoding.UTF8);
            }
            base.OnException(context);
        }

        protected ActionResult AjaxResponse(bool success, dynamic result)
        {
            return Content(new AjaxResponse { Success = success, Result = result }.ToJson(), "application/json", Encoding.UTF8);
        }
    }
}