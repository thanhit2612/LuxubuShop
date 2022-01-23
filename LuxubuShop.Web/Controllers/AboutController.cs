using LuxubuShop.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxubuShop.Web.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Detail()
        {
            var about = new AboutDao().ViewDetail();
            return View(about);
        }
    }
}