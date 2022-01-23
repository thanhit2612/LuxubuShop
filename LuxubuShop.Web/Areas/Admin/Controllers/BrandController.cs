using LuxubuShop.Core.Dao.ProductModel;
using LuxubuShop.Core.EF;
using LuxubuShop.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
    public class BrandController : BaseController
    {
		// GET: Admin/Brand
		public ActionResult Index()
		{
			var model = new BrandDao().ListAll();
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
		public ActionResult Create(Brand brand)
		{
			if (ModelState.IsValid)
			{
				var session = (UserLogin)Session[CommonConstants.USER_SESSION];
				brand.CreatedBy = session.UserName;
				long id = new BrandDao().Insert(brand);
				if (id > 0)
				{
					SetAlert("Thêm mới thành công !", "success");
					return RedirectToAction("Create", "Brand");
				}
			}
			return View("Index");
		}
		// GET: Admin/User/Create
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var brand = new BrandDao().GetByID(id);
			return View(brand);
		}
		// POST: Admin/User/Create
		[HttpPost]
		public ActionResult Edit(Brand brand)
		{
			if (ModelState.IsValid)
			{
				var result = new BrandDao().Update(brand);
				if (result)
				{
					SetAlert("Thêm mới thành công !", "success");
					return RedirectToAction("Index", "Brand");
				}
			}
			return View("Index");
		}
		// DELETE: Admin/User/Delete
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			new BrandDao().Delete(id);
			return RedirectToAction("Index");
		}
	}
}