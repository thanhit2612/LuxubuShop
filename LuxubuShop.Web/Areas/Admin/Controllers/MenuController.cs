using LuxubuShop.Core.Dao;
using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using System.Web.Mvc;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
	public class MenuController : BaseController
	{
		// Lấy ra danh sách sản phẩm
		public void SetViewBag(long? selectedId = null)
		{
			var dao = new MenuTypeDao();
			ViewBag.TypeID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
		}
		// GET: Admin/Menu
		public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
		{
			var dao = new MenuDao();
			var model = dao.ListAllPaging(searchString, page, pageSize);
			ViewBag.SearchString = searchString;
			return View(model);
		}

		// GET: Admin/Menu/Create
		[HttpGet]
		public ActionResult Create()
		{
			SetViewBag();
			return View();
		}

		// POST: Admin/Menu/Create
		[HttpPost]
		public ActionResult Create(Menu menu)
		{
			if (ModelState.IsValid)
			{
				long id = new MenuDao().Insert(menu);
				if (id > 0)
				{
					SetAlert("Thêm mới thành công !", "success");
					return RedirectToAction("Create", "Menu");
				}
				else
				{
					SetAlert("Thêm mới thất bại !", "danger");
				}
			}
			SetViewBag();
			return View("Index");
		}

		// GET: Admin/Menu/Edit/5
		public ActionResult Edit(int id)
		{
			var dao = new MenuDao();
			var menu = dao.ViewDetail(id);

			var typeId = dao.GetMenuByID(id);
			SetViewBag(typeId.TypeID);
			return View(menu);
		}

		// POST: Admin/Menu/Edit/5
		[HttpPost]
		public ActionResult Edit(Menu model)
		{
			if (ModelState.IsValid)
			{
				var result = new MenuDao().Update(model);
				if (result)
				{
					SetAlert("Cập nhật thành công !", "success");
					return RedirectToAction("Index", "Menu");
				}
				else
				{
					SetAlert("Cập nhật thất bại !", "error");
				}
			}
			SetViewBag(model.TypeID);
			return View("Index");
		}

		// POST: Admin/Menu/Delete/5
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			try
			{
				new MenuDao().Delete(id);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
