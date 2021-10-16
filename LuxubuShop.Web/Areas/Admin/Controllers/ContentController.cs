using LuxubuShop.Core.Dao;
using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            SetViewBag();
            return View(model);
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductDao();
            ViewBag.ProductID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
		}
        // GET: Admin/Content/Create
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        // POST: Admin/Content/Create
        [HttpPost]
        public ActionResult Create(Content content)
        {
			if (ModelState.IsValid)
			{
                var dao = new ContentDao();
                long id = dao.Insert(content);
                if (id > 0)
                {
                    SetAlert("Thêm mới bài viết thành công !", "success");
                    return RedirectToAction("Create", "Content");
                }
                else
                {
                    SetAlert("Thêm mới bài viết thất bại !", "danger");
                }
            }
            SetViewBag();
            return View("Index");
        }

        // GET: Admin/Content/Edit/5
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetContentByID(id);
            SetViewBag(content.ProductID);
            return View();
        }

        // POST: Admin/Content/Edit/5
        [HttpPost]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {

            }
            SetViewBag(model.ProductID);
            return View();
        }

        // POST: Admin/Content/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                new ContentDao().Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
