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
	public class AboutController : BaseController
	{
		// GET: Admin/About
		public ActionResult Index()
		{
			return View();
		}

		// GET: Admin/About/Details/5
		public ActionResult Details()
		{
			var about = new AboutDao().ViewDetail();
			return View(about);
		}

		// GET: Admin/About/Edit/5
		public ActionResult Edit()
		{
			var about = new AboutDao().ViewDetail();
			return View(about);
		}

		// POST: Admin/About/Edit/5
		[HttpPost, ValidateInput(false)]
		public ActionResult Edit(About model)
		{
			if (ModelState.IsValid)
			{
				var dao = new AboutDao();
				var result = dao.Update(model);
				if (result)
				{
					SetAlert("Cập nhật thành công !", "success");
					return RedirectToAction("Details", "About");
				}
			}
			return View("Index");
		}
	}
}
