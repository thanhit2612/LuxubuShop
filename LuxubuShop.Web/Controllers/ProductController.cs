using LuxubuShop.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using LuxubuShop.Core.EF;
using LuxubuShop.Core.Dao.ProductModel;

namespace LuxubuShop.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int page = 1, int pageSize = 20)
        {
            var model = new ProductDao().ListAll().OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return View(model);
        }
        // Hiển thị danh mục sản phẩm
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public PartialViewResult Category()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public PartialViewResult Brand()
        {
            var model = new BrandDao().ListAll();
            return PartialView(model);
        }
        // Lấy ra sản phẩm cho từng danh mục
        [OutputCache(Duration = int.MaxValue, VaryByParam = "id")]
        public ActionResult ProductCategory(long id, int page = 1, int pageSize = 20)
		{
            var productCategory = new ProductCategoryDao().GetById(id);
            ViewBag.ProductCategory = productCategory;

            var model = new ProductDao().ListByCategoryId(id).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return View(model);
		}
        // Hiển thị danh sách tên sp
        // Tìm kiếm sản phẩm
        public ActionResult Search(string q, int page = 1, int pageSize = 20)
        {
            var dao = new ProductDao();
            var model = dao.Search(q, page, pageSize);

            ViewBag.KeyWord = q;
            return View(model);
        }
        // Click Count
        [HttpPost]
        public JsonResult ClickCount(long id)
        {
            var result = new ProductDao().ClickCount(id);
            return Json(new
            {
                count = result,
            });
        }
    }
}