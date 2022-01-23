using LuxubuShop.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LuxubuShop.Web.Controllers
{
	public class BaseController : Controller
	{
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var session = (UserLogin)Session[CommonConstants.CUSTOMER_SESSION];
			if (session == null)
			{
				filterContext.Result = new RedirectToRouteResult(new
					RouteValueDictionary(new { controller = "User", Action = "Login" }));
			}
			base.OnActionExecuting(filterContext);
		}
		protected void SetAlert(string message, string type)
		{
			TempData["AlertMessage"] = message;
			if (type == "danger")
			{
				TempData["AlertType"] = "toast--danger";
				TempData["AlertIcon"] = "fa fa-exclamation-triangle";
			}
			else if (type == "success")
			{
				TempData["AlertType"] = "toast--success";
				TempData["AlertIcon"] = "fa fa-check-circle";
			}
		}
	}
}