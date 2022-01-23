using LuxubuShop.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using LuxubuShop.Core.EF;
using LuxubuShop.Web.Common;

namespace LuxubuShop.Web.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index(string tagId, int page = 1, int pageSize = 10)
        {
            var model = new ContentDao().ListAll().OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return View(model);
        }
        // Danh sách bài viết theo sản phẩm
        public ActionResult ProductContent(string tagId, long id, int page = 1, int pageSize = 10)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Product = product;

            var model = new ContentDao().ListByProductId(id).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            return View(model);
        }
        // Chi tiết bài viết
        public ActionResult Details(long id)
        {
            var content = new ContentDao().Detail(id);
            ViewBag.ListRelatedContent = new ContentDao().ListRelatedContent(id, 10);
            ViewBag.Tags = new ContentDao().ListTag(id);


            return View(content);
        }
        // Lấy ra bài viết từ tags
        public ActionResult Tag(string tagId, int page = 1, int pageSize = 10)
		{
            var model = new ContentDao().ListAllByTag(tagId, page, pageSize).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            ViewBag.Tag = new ContentDao().GetTag(tagId);
            return View(model);
		}

        [HttpPost]
        public JsonResult ClickCount(long id)
        {
            var result = new ContentDao().ClickCount(id);
            return Json(new
            {
                count = result,
            });
        }
    }
}