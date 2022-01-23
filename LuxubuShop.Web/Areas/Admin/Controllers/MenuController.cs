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
		// GET: Admin/Menu
		public ActionResult Index()
		{
			var dao = new MenuDao();
			var model = dao.ListAll();
			return View(model);
		}

		// GET: Admin/Menu/Create
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		// POST: Admin/Menu/Create
		[HttpPost, ValidateInput(false)]
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
		
			return View("Index");
		}

		// GET: Admin/Menu/Edit/5
		public ActionResult Edit(int id)
		{
			var dao = new MenuDao();
			var menu = dao.GetMenuByID(id);

			var typeId = dao.GetMenuByID(id);
		
			return View(menu);
		}

		// POST: Admin/Menu/Edit/5
		[HttpPost, ValidateInput(false)]
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
