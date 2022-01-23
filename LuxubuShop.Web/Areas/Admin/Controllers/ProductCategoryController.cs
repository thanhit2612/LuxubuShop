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
	public class ProductCategoryController : BaseController
	{
		// GET: Admin/ProductCategory
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
		{
			var dao = new ProductCategoryDao();
			var model = dao.AdminListAll(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			
			return View(model);
		}

		// GET: Admin/ProductCategory/Create
		[HttpGet]
		public ActionResult Create()
		{
			
			return View();
		}

		// POST: Admin/ProductCategory/Create
		[HttpPost, ValidateInput(false)]
		public ActionResult Create(ProductCategory proCategory)
		{
			if (ModelState.IsValid)
			{
				var dao = new ProductCategoryDao();
				var session = (UserLogin)Session[CommonConstants.USER_SESSION];
				proCategory.CreatedBy = session.UserName;
				proCategory.CreatedDate = DateTime.Now;
				long id = dao.Insert(proCategory);
				if (id > 0)
				{
					SetAlert("Thêm mới danh mục thành công !", "success");
					return RedirectToAction("Create", "ProductCategory");
				}
				else
				{
					SetAlert("Thêm mới danh mục thất bại !", "error");
				}
			}
		
			return View("Index");
		}
		// GET: Admin/ProductCategory/Edit/
		public ActionResult Edit(int id)
		{
			var dao = new ProductCategoryDao();
			var proCategory = dao.GetById(id);

			return View(proCategory);
		}

		// POST: Admin/ProductCategory/Edit/
		[HttpPost, ValidateInput(false)]
		public ActionResult Edit(ProductCategory model)
		{
			if (ModelState.IsValid)
			{
				var dao = new ProductCategoryDao();
				var result = dao.Update(model);
				if (result)
				{
					SetAlert("Cập nhật danh mục thành công !", "success");
					return RedirectToAction("Index", "ProductCategory");
				}
				else
				{
					SetAlert("Cập nhật danh mục thất bại !", "error");
				}
			}
		
			return View("Index");
		}

		// POST: Admin/ProductCategory/Delete/
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			try
			{
				new ProductCategoryDao().Delete(id);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}