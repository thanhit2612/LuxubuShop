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
        // Lấy ra danh sách sản phẩm
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductDao();
            ViewBag.ProductID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            SetViewBag();
            return View(model);
        }

        // GET: Admin/Content/Create
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        // POST: Admin/Content/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Content content)
        {
			if (ModelState.IsValid)
			{
                var dao = new ContentDao();
                TempData.Keep("UserName");
                var userName = TempData["UserName"];
                content.CreatedBy = (string)userName;
                content.CreatedDate = DateTime.Now;
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
        public ActionResult Edit(int id)
        {
            var dao = new ContentDao();
            var content = dao.ViewDetail(id);

            var productId = dao.GetContentByID(id);
            SetViewBag(productId.ProductID);
            return View(content);
        }

        // POST: Admin/Content/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Cập nhật bài viết thành công !", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    SetAlert("Cập nhật bài viết thất bại !", "error");
                }
            }
            SetViewBag(model.ProductID);
            return View("Index");
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
