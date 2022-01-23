using LuxubuShop.Core.EF;
using PagedList;
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
		// Get Method By ID
		public List<Menu> ListAll()
		{
			return db.Menus.Where(x => x.Status == true).ToList();
		}

		// Insert Method
		public long Insert(Menu menu)
		{
			menu.Status = true;
			db.Menus.Add(menu);
			db.SaveChanges();
			return menu.ID;
		}

		// Update Method
		public Menu GetMenuByID(long id)
		{
			return db.Menus.Find(id);
		}
		public bool Update(Menu entity)
		{
			try
			{
				var menu = db.Menus.Find(entity.ID);
				menu.Text = entity.Text;
				menu.Link = entity.Link;
				menu.Icon = entity.Icon;
				menu.Status = true;

				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		// Delete Method
		public bool Delete(int id)
		{
			try
			{
				var menu = db.Menus.Find(id);
				db.Menus.Remove(menu);
				db.SaveChanges();
				return true;

			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
