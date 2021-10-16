using LuxubuShop.Core.Dao;
using LuxubuShop.Web.Areas.Admin.Data;
using LuxubuShop.Web.Common;
using System.Web.Mvc;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
	public class LoginController : Controller
	{
		// GET: Admin/Login
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var dao = new UserDao();
				var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
				if (result)
				{
					var user = dao.GetById(model.UserName);
					var userSession = new UserLogin();
					userSession.UserName = user.UserName;
					userSession.UserID = user.ID;
					Session.Add(CommonConstants.USER_SESSION, userSession);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng !");
				}
			}
			return View("Index");
		}
		public ActionResult Logout()
		{
			return RedirectToAction("Index");
		}
	}
}