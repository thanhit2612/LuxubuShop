using LuxubuShop.Core.Dao;
using LuxubuShop.Core.EF;
using LuxubuShop.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
	public class SlideController : BaseController
	{
		// GET: Admin/Slide
		public ActionResult Index()
		{
			var model = new SlideDao().ListAll();
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
		public ActionResult Create(Slide slide)
		{
			if (ModelState.IsValid)
			{
				var session = (UserLogin)Session[CommonConstants.USER_SESSION];
				slide.CreatedBy = session.UserName;
				long id = new SlideDao().Insert(slide);
				if (id > 0)
				{
					SetAlert("Thêm mới thành công !", "success");
					return RedirectToAction("Create", "Slide");
				}
			}
			return View("Index");
		}
		// GET: Admin/User/Create
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var slide = new SlideDao().GetByID(id);
			return View(slide);
		}
		// POST: Admin/User/Create
		[HttpPost]
		public ActionResult Edit(Slide slide)
		{
			if (ModelState.IsValid)
			{
				var result = new SlideDao().Update(slide);
				if (result)
				{
					SetAlert("Thêm mới thành công !", "success");
					return RedirectToAction("Index", "Slide");
				}
			}
			return View("Index");
		}
		// DELETE: Admin/User/Delete
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			new SlideDao().Delete(id);
			return RedirectToAction("Index");
		}

	}
}