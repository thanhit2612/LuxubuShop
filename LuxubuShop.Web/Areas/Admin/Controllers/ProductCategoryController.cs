using LuxubuShop.Core.Dao;
using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
	public class ProductCategoryController : BaseController
	{
		// Lấy ra danh sách danh mục cha
		public void SetViewBag(long? selectedId = null)
		{
			var dao = new ProductCategoryDao();
			ViewBag.ParentID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
		}
		// GET: Admin/ProductCategory
		public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
		{
			var dao = new ProductCategoryDao();
			var model = dao.ListAllPaging(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			SetViewBag();
			return View(model);
		}

		// GET: Admin/ProductCategory/Create
		[HttpGet]
		public ActionResult Create()
		{
			SetViewBag();
			return View();
		}

		// POST: Admin/ProductCategory/Create
		[HttpPost, ValidateInput(false)]
		public ActionResult Create(ProductCategory proCategory)
		{
			if (ModelState.IsValid)
			{
				var dao = new ProductCategoryDao();
				TempData.Keep("UserName");
				var userName = TempData["UserName"];
				proCategory.CreatedBy = (string)userName;
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
			SetViewBag();
			return View("Index");
		}
		// GET: Admin/ProductCategory/Edit/
		public ActionResult Edit(int id)
		{
			var dao = new ProductCategoryDao();
			var proCategory = dao.ViewDetail(id);

			var parentID = dao.ParentID(id);
			SetViewBag(parentID.ParentID);
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
			SetViewBag(model.ParentID);
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