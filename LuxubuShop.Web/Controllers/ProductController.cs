using LuxubuShop.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using LuxubuShop.Core.EF;

namespace LuxubuShop.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            var model = new ProductDao().ListAll().OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return View(model);
        }
        // Hiển thị danh mục sản phẩm
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
        // Lấy ra sản phẩm cho từng danh mục
        public ActionResult Category(long id, int page = 1, int pageSize = 12)
		{
            var category = new ProductCategoryDao().ViewDetail(id);
            ViewBag.Category = category;

            var model = new ProductDao().ListByCategoryId(id).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return View(model);
		}
        // Hiển thị danh sách tên sp
        public JsonResult ListName(string q)
		{
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
             status = true,
            }, JsonRequestBehavior.AllowGet);
		}
        // Tìm kiếm sản phẩm
        public ActionResult Search(string q, int page = 1, int pageSize = 12)
        {
            var dao = new ProductDao();
            var model = dao.Search(q, page, pageSize);

            ViewBag.KeyWord = q;
            return View(model);
        }
        // Click Count
        public ActionResult ClickCount(long id)
        {
            var count = new ProductDao().ClickCount(id);
            return RedirectToAction("Index","Content", count);
        }
    }
}