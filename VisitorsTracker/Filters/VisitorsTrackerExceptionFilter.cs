using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VisitorsTracker.Core.Exceptions;

namespace VisitorsTracker.Filters
{
    public class VisitorsTrackerExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Result is VisitorsTrackerException eventsExpressException)
            {
                context.ModelState.AddModelError(string.Empty, eventsExpressException.Message);
                var result = new ObjectResult(context.ModelState) { StatusCode = 400 };
                context.Result = result;
                context.ExceptionHandled = true;
            }
            else
            {
                string message = "Unhandled exception occurred. Please try again. " +
                    "If this error persists - contact system administrator.";
                context.ModelState.AddModelError(string.Empty, message);
                var result = new ObjectResult(context.ModelState) { StatusCode = 500 };
                context.Result = result;
                context.ExceptionHandled = true;
            }
        }
    }
}
