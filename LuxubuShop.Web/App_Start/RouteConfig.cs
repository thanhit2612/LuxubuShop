using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LuxubuShop.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// Danh sách sản phẩm theo danh mục
			routes.MapRoute(
				name: "Product Category",
				url: "san-pham/{metaTitle}-{id}",
				defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
				namespaces: new[] { "LuxubuShop.Web.Controllers" }
			);
			// Danh sách tất cả sản phẩm
			routes.MapRoute(
				name: "Product",
				url: "san-pham",
				defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "LuxubuShop.Web.Controllers" }
			);
			// Danh sách bài viết theo từng sản phẩm
			routes.MapRoute(
				name: "Product Content",
				url: "danh-gia/{metaTitle}-{id}",
				defaults: new { controller = "Content", action = "ProductContent", id = UrlParameter.Optional },
				namespaces: new[] { "LuxubuShop.Web.Controllers" }
			);
			// Danh sách tất cả bài viết 
			routes.MapRoute(
			  name: "Content",
			  url: "danh-gia",
			  defaults: new { controller = "Content", action = "Index", id = UrlParameter.Optional },
			  namespaces: new[] { "LuxubuShop.Web.Controllers" }
			);
			// Chi tiết bài viết
			routes.MapRoute(
			  name: "Content Details",
			  url: "danh-gia/chi-tiet/{metaTitle}-{id}",
			  defaults: new { controller = "Content", action = "Details", id = UrlParameter.Optional },
			  namespaces: new[] { "LuxubuShop.Web.Controllers" }
			);
			// Giới thiệu
			routes.MapRoute(
			  name: "About",
			  url: "gioi-thieu/{id}",
			  defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
			  namespaces: new[] { "LuxubuShop.Web.Controllers" }
		  );
			// Đăng nhập
			routes.MapRoute(
			  name: "Login",
			  url: "dang-nhap",
			  defaults: new { controller = "Login", action = "LoginFacebook", id = UrlParameter.Optional },
			  namespaces: new[] { "LuxubuShop.Web.Controllers" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "LuxubuShop.Web.Controllers" }
			);
		}
	}
}
