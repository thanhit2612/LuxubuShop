using LuxubuShop.Core.Dao;
using LuxubuShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxubuShop.Web.Controllers
{

    public class WishlistController : BaseController
    {
        // GET: Wishest

        public ActionResult Index()
        {
            var wishlist = Session[Common.CommonConstants.WishlistSession];
            var list = new List<WishlistItem>();
			if (wishlist != null)
			{
                list = (List<WishlistItem>)wishlist;
			}
            return View(list);
        }

		public ActionResult AddItem(long productId, int quantity)
		{
			var product = new ProductDao().ViewDetail(productId);
			var wishlist = Session[Common.CommonConstants.WishlistSession];

			if (wishlist != null)
			{
				var list = (List<WishlistItem>)wishlist;
				if (list.Exists(x => x.Product.ID == productId))
				{
					foreach (var item in list)
					{
						if (item.Product.ID == productId)
						{
							SetAlert("Sản phẩm đã tồn tại trong danh sách yêu thích !", "danger");
						}
					}
				}
				else
				{
					var item = new WishlistItem();
					item.Product = product;
					item.Quantity = quantity;
					list.Add(item);
				}
				Session[Common.CommonConstants.WishlistSession] = list;
			}
			else
			{
				var item = new WishlistItem();
				item.Product = product;
				item.Quantity = quantity;
				var list = new List<WishlistItem>();
				list.Add(item);

				Session[Common.CommonConstants.WishlistSession] = list;
				SetAlert("Thêm vào danh sách yêu thích thành công !", "success");
			}
			return Redirect(Request.UrlReferrer.ToString());
		}
		public JsonResult Delete(long id)
		{
             var sessionWishlist = (List<WishlistItem>)Session[Common.CommonConstants.WishlistSession];
                sessionWishlist.RemoveAll(x => x.Product.ID == id);
				SetAlert("Xóa sản phẩm thành công !", "success");
				Session[Common.CommonConstants.WishlistSession] = sessionWishlist;
				return Json(new {
					status = true
				});
        }
    }
}