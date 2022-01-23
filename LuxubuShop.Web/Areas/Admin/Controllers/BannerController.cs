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
	public class BannerController : BaseController
	{
		// GET: Admin/Banner
		public ActionResult Index()
		{
			var dao = new BannerDao();
			var model = dao.ListAll();
			return View(model);
		}

		// GET: Admin/Banner/Create
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		// POST: Admin/Banner/Create
		[HttpPost]
		public ActionResult Create(Banner banner)
		{
			if (ModelState.IsValid)
			{
				long id = new BannerDao().Insert(banner);
				if (id > 0)
				{
					SetAlert("Thêm mới thành công !", "success");
					return RedirectToAction("Create", "Banner");
				}
				else
				{
					SetAlert("Thêm mới thất bại !", "danger");
				}
			}

			return View("Index");
		}

		// GET: Admin/Banner/Edit/5
		public ActionResult Edit(int id)
		{
			var dao = new BannerDao();
			var banner = dao.GetByID(id);

			return View(banner);
		}

		// POST: Admin/Banner/Edit/5
		[HttpPost]
		public ActionResult Edit(Banner model)
		{
			if (ModelState.IsValid)
			{
				var result = new BannerDao().Update(model);
				if (result)
				{
					SetAlert("Cập nhật thành công !", "success");
					return RedirectToAction("Index", "Banner");
				}
				else
				{
					SetAlert("Cập nhật thất bại !", "error");
				}
			}

			return View("Index");
		}

		// POST: Admin/Banner/Delete/5
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			try
			{
				new BannerDao().Delete(id);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}