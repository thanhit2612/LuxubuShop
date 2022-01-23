using LuxubuShop.Core.Dao;
using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using LuxubuShop.Web.Common;
using PagedList;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // Lấy ra danh sách sản phẩm
        public void SetCatID(long? selectedId = null)
        {
            ViewBag.CategoryID = new SelectList(new ProductCategoryDao().ListAll(), "ID", "Name", selectedId);
        }
        public void SetProID(long? selectedId = null)
        {
            ViewBag.ProductID = new SelectList(new ProductDao().ListAll(), "ID", "Name", selectedId);
        }

        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10, long catID = 0)
        {
            ViewBag.CategoryID = new ProductCategoryDao().ListAll();

            var dao = new ContentDao();
            var model = dao.AdminListAll(searchString, catID).ToPagedList(page, pageSize); ;
            ViewBag.SearchString = searchString;

            return View(model);
        }

        // GET: Admin/Content/Create
        [HttpGet]
        public ActionResult Create()
        {
            SetCatID();
            SetProID();
            return View();
        }

        // POST: Admin/Content/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Content content)
        {
			if (ModelState.IsValid)
			{
                var dao = new ContentDao();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                content.CreatedBy = session.UserName;
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
            SetCatID();
            SetProID();
            return View("Index");
        }

        // GET: Admin/Content/Edit/5
        public ActionResult Edit(int id)
        {
            var dao = new ContentDao();
            var content = dao.ViewDetail(id);
            var productId = dao.GetContentByID(id);

            SetCatID(productId.CategoryID);
            SetProID(productId.ProductID);
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
            SetCatID(model.CategoryID);
            SetProID(model.ProductID);
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
        [HttpPost]
        public JsonResult ChangeTopHot(long id)
        {
            var result = new ContentDao().ChangeTopHot(id);
            return Json(new
            {
                topHot = result,
            });
        }
    }
}
