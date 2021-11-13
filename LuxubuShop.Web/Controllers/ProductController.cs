using LuxubuShop.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

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
        public ActionResult Category(long id, int page = 1, int pageSize = 1)
		{
            var category = new ProductCategoryDao().ViewDetail(id);
            ViewBag.Category = category;

            var model = new ProductDao().ListByCategoryId(id).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return View(model);
		}
    }
}