using LuxubuShop.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace LuxubuShop.Web.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var model = new ContentDao().ListAll().OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return View(model);
        }
        // Danh sách bài viết theo sản phẩm
        public ActionResult ProductContent(long id, int page = 1, int pageSize = 1)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Product = product;

            var model = new ContentDao().ListByProductId(id).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return View(model);
        }
    }
}