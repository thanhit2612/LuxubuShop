using Facebook;
using LuxubuShop.Core.Dao;
using LuxubuShop.Core.EF;
using LuxubuShop.Web.Common;
using LuxubuShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxubuShop.Web.Controllers
{
	public class UserController : Controller
	{
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

		[HttpGet]
		[OutputCache(Duration = 3600)]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[OutputCache(Duration = 3600)]
		public ActionResult Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				var dao = new UserDao();

				if (dao.CheckUserName(model.UserName))
				{
					ModelState.AddModelError("", "Tài khoản đã tồn tại");
				}
				else if (dao.CheckEmail(model.Email))
				{
					ModelState.AddModelError("", "Email đã tồn tại");
				}
				else
				{
					var user = new User();
					user.UserName = model.UserName;
					var encryptedMd5Pas = Encryptor.MD5Hash(model.Password);
					user.Password = encryptedMd5Pas;
					user.Email = model.Email;
					user.Name = model.Name;
					user.Status = true;
					user.GroupID = "MEMBER";
					user.CreatedDate = DateTime.Now;

					var result = dao.Insert(user);
					if (result > 0)
					{
						ViewBag.Success = "Tạo tài khoản thành công !";
					}
					else
					{
						ModelState.AddModelError("", "Đăng ký không thành công");
					}
				}
			}
			return View(model);
		}


		[HttpGet]
		[OutputCache(Duration = 3600)]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[OutputCache(Duration = 3600)]
		public ActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var dao = new UserDao();
				var result = dao.LoginUser(model.UserName, Encryptor.MD5Hash(model.Password), true);
				if (result == 1)
				{
					var user = dao.GetById(model.UserName);
					var userSession = new UserLogin();
					userSession.UserName = user.UserName;
					userSession.UserID = user.ID;
					userSession.Name = user.Name;
					userSession.Image = user.Image;
					userSession.Email = user.Email;


					Session.Add(CommonConstants.CUSTOMER_SESSION, userSession);
					SetAlert("Đăng nhập thành công", "success");
					return RedirectToAction("Index", "Home");
				}
				else if (result == 0)
				{
					ModelState.AddModelError("", "Tài khoản không tồn tại !");
				}
				else if (result == -2)
				{
					ModelState.AddModelError("", "Mật khẩu không chính xác !");
				}
				else
				{
					ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng !");
				}
			}
			return View();
		}

		/*Login Facebook*/
		private Uri RedirectUri
		{
			get
			{
				var uriBuilder = new UriBuilder(Request.Url);
				uriBuilder.Query = null;
				uriBuilder.Fragment = null;
				uriBuilder.Path = Url.Action("FacebookCallback");
				return uriBuilder.Uri;
			}
		}
		[OutputCache(Duration = 3600)]
		public ActionResult LoginFacebook()
		{
			var fb = new FacebookClient();
			var loginUrl = fb.GetLoginUrl(new
			{
				client_id = ConfigurationManager.AppSettings["FbAppId"],
				client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
				redirect_uri = RedirectUri.AbsoluteUri,
				response_type = "code",
				scope = "email"
			});
			return Redirect(loginUrl.AbsoluteUri);
		}
		public ActionResult FacebookCallback(string code)
		{
			var fb = new FacebookClient();
			dynamic result = fb.Post("oauth/access_token", new
			{
				client_id = ConfigurationManager.AppSettings["FbAppId"],
				client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
				redirect_uri = RedirectUri.AbsoluteUri,
				code = code,
			});
			var accessToken = result.access_token;
			if (!string.IsNullOrEmpty(accessToken))
			{
				fb.AccessToken = accessToken;
				dynamic me = fb.Get("me?fields=id,name,email,picture");
				string email = me.email;
				string userName = me.email;
				string name = me.name;
				string picUrl = me.picture.data.url;

				var user = new User();
				user.Email = email;
				user.UserName = userName;
				user.Name = name;
				user.Image = picUrl;
				user.Status = true;
				user.CreatedDate = DateTime.Now;

				var resultInsert = new UserDao().InsertForFacebook(user);
				if (resultInsert > 0)
				{
					var userSession = new UserLogin();
					userSession.UserName = user.UserName;
					userSession.Name = user.Name;
					userSession.UserID = user.ID;
					userSession.Email = user.Email;
					userSession.Image = user.Image;
					Session.Add(CommonConstants.CUSTOMER_SESSION, userSession);
				}
			}
			return Redirect("/");
		}
		// Loout
		public ActionResult Logout()
		{
			Session[CommonConstants.CUSTOMER_SESSION] = null;
			return Redirect("/");
		}
		// ForgetPassword
		[HttpGet]
		[OutputCache(Duration = 3600)]
		public ActionResult ForgotPassword()
		{
			return View();
		}
		// ForgetPassword
		[HttpPost]
		[OutputCache(Duration = 3600)]
		public ActionResult ForgotPassword(ForgotPasswordModel model)
		{

			if (ModelState.IsValid)
			{
				var dao = new UserDao();
				if (dao.CheckUserName(model.UserName))
				{
					var user = new User();
					user.UserName = model.UserName;
					var encryptedMd5Pas = Encryptor.MD5Hash(model.Password);
					user.Password = encryptedMd5Pas;
					user.Status = true;

					var result = dao.ForgotPassWord(user);
					if (result)
					{
						ViewBag.Success = "Thay đổi mật khẩu thành công !";
					}
				} 
			else
			{
				ModelState.AddModelError("", "Tài khoản không tồn tại !");
			}
			}
			return View(model);
		}
	}
}