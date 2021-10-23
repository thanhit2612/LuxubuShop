using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao
{
	public class ProductDao
	{
		LuxubuShopDbContext db = null;
		public ProductDao()
		{
			db = new LuxubuShopDbContext();
		}
		// Danh sách sản phẩm
		public List<Product> ListAll()
		{
			return db.Products.Where(x => x.Status == true).ToList();
		}
		// Danh sách sản phẩm mới nhât
		public List<Product> ListNewProduct(int top)
		{
			return db.Products.OrderBy(x => x.CreatedDate).Take(top).ToList();
		}
		// Danh sách sản phẩm nổi bật
		public List<Product> ListNewFeature(int top)
		{
			return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderBy(x => x.CreatedDate).Take(top).ToList();
		}
	}
}
