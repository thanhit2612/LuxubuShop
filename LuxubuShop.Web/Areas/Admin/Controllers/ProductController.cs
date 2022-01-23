using LuxubuShop.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuxubuShop.Core.EF;
using LuxubuShop.Web.Common;
using PagedList;
using LuxubuShop.Core.Dao.ProductModel;

namespace LuxubuShop.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // Lấy ra danh sách danh mục
        public void SetCategory(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        public void SetBrand(long? selectedId = null)
        {
            var dao = new BrandDao();
            ViewBag.BrandID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10, long catID = 0)
        {
            ViewBag.CategoryID = new ProductCategoryDao().ListAll();

            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, catID).ToPagedList(page, pageSize); ;
            ViewBag.SearchString = searchString;

            return View(model);
        }
        // GET: Admin/Content/Create
        [HttpGet]
        public ActionResult Create()
        {
            SetCategory();
            SetBrand();
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
            SetCategory();
            SetBrand();
            return View("Index");
        }

        // GET: Admin/Content/Edit/5
        public ActionResult Edit(int id)
        {
            var dao = new ProductDao();
            var product = dao.ViewDetail(id);

            var categoryId = dao.GetProductCategoryByID(id);
            SetCategory(product.CategoryID);
            SetBrand(product.BrandID);
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
            SetCategory(model.CategoryID);
            SetBrand(model.BrandID);
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

        public ActionResult ListName()
		{
            var data = new ProductDao().ListAll();
            return Json(new
            {
                data = data,
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangeTopHot(long id)
        {
            var result = new ProductDao().ChangeTopHot(id);
            return Json(new
            {
                topHot = result,
            });
        }
    }
}