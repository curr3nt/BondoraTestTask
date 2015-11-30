using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Frontend.BackendWrapper;
using Frontend.CustomExceptions;

namespace Frontend.Controllers
{
    public abstract class ABaseController : Controller
    {
        protected IBackendServiceWrapper Service;

        protected ABaseController()
        {
            Service = new BackendTcpServiceWrapper("127.0.0.1", 10001);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            if (exception is ABackendException)
            {
                var message = exception.Message;
                TempData["ErrorMessage"] = message;
                filterContext.ExceptionHandled = true;
                filterContext.Result 
                    = new RedirectToRouteResult(new RouteValueDictionary(
                        new { controller = "Home", action = "Index" }));
            }
        }
    }
}
