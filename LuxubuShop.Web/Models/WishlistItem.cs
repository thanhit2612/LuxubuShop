using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxubuShop.Web.Models
{
	[Serializable]
	public class WishlistItem
	{
		public Product  Product { get; set; }
		public int Quantity { get; set; }
	}
}