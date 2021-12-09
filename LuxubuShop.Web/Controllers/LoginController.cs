using Facebook;
using LuxubuShop.Core.Dao;
using LuxubuShop.Core.EF;
using LuxubuShop.Web.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxubuShop.Web.Controllers
{
	public class LoginController : Controller
	{
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
		// GET: Login
		public ActionResult Index()
		{
			return View();
		}
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
				dynamic me = fb.Get("me?fields=id,name,email");
				string email = me.email;
				string userName = me.email;
				string name = me.name;

				var user = new User();
				user.Email = email;
				user.UserName = userName;
				user.Status = true;
				user.Name = name;
				user.CreatedDate = DateTime.Now;

				var resultInsert = new UserDao().InsertForFacebook(user);
				if (resultInsert > 0)
				{
					var userSession = new UserLogin();
					userSession.UserName = user.UserName;
					userSession.UserID = user.ID;
					Session.Add(CommonConstants.USER_SESSION, userSession);
				}
			}
			return Redirect("/");
		}
		public ActionResult Logout()
		{
			Session[CommonConstants.USER_SESSION] = null;
			return Redirect("/");
		}
	}
}