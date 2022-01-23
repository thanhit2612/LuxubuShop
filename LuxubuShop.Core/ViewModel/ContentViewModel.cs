using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.ViewModels
{
	public class ContentViewModel
	{
		public long ID { set; get; }
		public long ProductID { get; set; }
		public string Name { set; get; }
		public string Description { get; set; }
		public string ProductLink { set; get; }
		public long BrandID { set; get; }
		public string BrandName { set; get; }
		public long? CategoryID { get; set; }
		public string CateName { get; set; }
		public string Detail { set; get; }
		public string Image { get; set; }
		public string Tags { get; set; }
		public string MetaTitle { get; set; }
		public string Keywords { get; set; }
		public string MetaDescriptions { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public int ViewCount { get; set; }
		public bool Status { get; set; }
		public bool TopHot { get; set; }
	}
}
