using LuxubuShop.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using LuxubuShop.Core.EF;
using LuxubuShop.Web.Common;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // Lấy ra danh sách danh mục
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
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
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                product.CreatedBy = session.UserName;

                long id = dao.Insert(product);
                if (id > 0)
                {
                    SetAlert("Thêm mới sản phẩm thành công !", "success");
                    return RedirectToAction("Create", "Product");
                }
                else
                {
                    SetAlert("Thêm mới sản phẩm thất bại !", "danger");
                }
            }
            SetViewBag();
            return View("Index");
        }

        // GET: Admin/Content/Edit/5
        public ActionResult Edit(int id)
        {
            var dao = new ProductDao();
            var product = dao.ViewDetail(id);

            var categoryId = dao.GetProductCategoryByID(id);
            SetViewBag(categoryId.CategoryID);
            return View(product);
        }

        // POST: Admin/Content/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Cập nhật sản phẩm thành công !", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Cập nhật sản phẩm thất bại !", "error");
                }
            }
            SetViewBag(model.CategoryID);
            return View("Index");
        }

        // POST: Admin/Content/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                new ProductDao().Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}