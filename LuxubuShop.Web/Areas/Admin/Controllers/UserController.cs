using LuxubuShop.Core.Dao;
using LuxubuShop.Core.EF;
using LuxubuShop.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using LuxubuShop.Core.Dao.UserModel;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
	public class UserController : BaseController
	{
		// GET: Admin/User
		public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
		{
			var dao = new UserDao();
			var model = dao.ListAllPaging(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			return View(model);
		}

		// GET: Admin/User/Create
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}
		// POST: Admin/User/Create
		[HttpPost]
		public ActionResult Create(User user)
		{
			if (ModelState.IsValid)
			{
				var dao = new UserDao();

				var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
				user.Password = encryptedMd5Pas;
				user.GroupID = "ADMIN";
				user.CreatedDate = DateTime.Now;
				long id = dao.Insert(user);

				if (id > 0)
				{
					SetAlert("Thêm mới tài khoản thành công !", "success");
					return RedirectToAction("Create", "User");
				}
				else
				{
					SetAlert("Thêm mới tài khoản thất bại !", "danger");
				}


			}
			return View("Index");
		}

		// GET: Admin/User/Edit
		public ActionResult Edit(int id)
		{
			var user = new UserDao().GetById(id);
			return View(user);
		}
		// POST: Admin/User/Edit
		[HttpPost]
		public ActionResult Edit(User user)
		{
			if (ModelState.IsValid)
			{
				if (!string.IsNullOrEmpty(user.Password))
				{
					var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
					user.Password = encryptedMd5Pas;
				}

				var dao = new UserDao();
				var result = dao.Update(user);
				if (result)
				{
					SetAlert("Cập nhật tài khoản thành công !", "success");
					return RedirectToAction("Index", "User");
				}
				else
				{
					SetAlert("Cập nhật tài khoản thất bại !", "danger");
				}
			}
			return View("Index");
		}

		// DELETE: Admin/User/Delete
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			new UserDao().Delete(id);
			return RedirectToAction("Index");
		}
	}
}