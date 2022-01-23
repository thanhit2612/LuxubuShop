using LuxubuShop.Core.Dao;
using LuxubuShop.Core.Dao.ProductModel;
using LuxubuShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxubuShop.Web.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            TempData["Category"] = new ProductCategoryDao().ListAll();
            TempData["Menu"] = new MenuDao().ListAll();
            TempData["Brand"] = new BrandDao().ListAll();

            ViewBag.Slides = new SlideDao().ListAll();
            ViewBag.Banners = new BannerDao().ListAll();

            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(10);
            ViewBag.FeatureProducts = productDao.ListFeature(10);

            ViewBag.FeatureContents = new ContentDao().ListFeature(8);
            return View();
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListAll();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult HeaderWishlist()
        {
            var wishlist = Session[Common.CommonConstants.WishlistSession];
            var list = new List<WishlistItem>();
            if (wishlist != null)
            {
                list = (List<WishlistItem>)wishlist;
            }
            return PartialView(list);
        }
    }
}