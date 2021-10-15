using LuxubuShop.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
	public class BaseController : Controller
	{
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var session = (UserLogin)Session[CommonConstants.USER_SESSION];
			if (session == null)
			{
				filterContext.Result = new RedirectToRouteResult(new
					RouteValueDictionary(new { controller = "Login", Action = "Index", Areas = "Admin" }));
			}
			base.OnActionExecuting(filterContext);
		}
		protected void SetAlert(string message, string type)
		{
			TempData["AlertMessage"] = message;
			if (type == "error")
			{
				TempData["AlertType"] = "alert-danger";
			}
			else if (type == "warning")
			{
				TempData["AlertType"] = "alert-warning";
			}
			else if (type == "success")
			{
				TempData["AlertType"] = "alert-success";
			}
		}

	}
}