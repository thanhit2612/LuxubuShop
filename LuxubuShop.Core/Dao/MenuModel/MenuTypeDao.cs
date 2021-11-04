using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao
{
	public class MenuTypeDao
	{
		LuxubuShopDbContext db = null;
		public MenuTypeDao()
		{
			db = new LuxubuShopDbContext();
		}
		public List<MenuType> ListAll()
		{
			return db.MenuTypes.Where(x => x.Status == true).ToList();
		}
	}
}
