using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao
{
	public class MenuDao
	{
		LuxubuShopDbContext db = null;
		public MenuDao()
		{
			db = new LuxubuShopDbContext();
		}
		public List<Menu> ListByGroupId(int groupId)
		{
			return db.Menus.Where(x => x.TypeID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
		}
	}
}
