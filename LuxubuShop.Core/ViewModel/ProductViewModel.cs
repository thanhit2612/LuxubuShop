using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.ViewModels
{
	public class ProductViewModel
	{
		public long ID { set; get; }
		public string Name { set; get; }
		public long? CategoryID { get; set; }
		public long? BrandID { get; set; }
		public string CateName { set; get; }
		public string Description { get; set; }
		public string Image { set; get; }
		public decimal? Price { set; get; }
		public decimal? PromotionPrice { get; set; }
		public string ProductLink { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ExpirationDate { get; set; }
		public string CreatedBy { get; set; }
		public string Keywords { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		public bool Status { get; set; }
		public int ViewCount { get; set; }
		public bool TopHot { get; set; }
		public int Rating { get; set; }
	}
}
