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
		OnlineShopDbContext db = null;
		public ProductDao()
		{
			db = new OnlineShopDbContext();
		}
		public List<Product> ListAll()
		{
			return db.Products.Where(x => x.Status == true).ToList();
		}
	}
}
