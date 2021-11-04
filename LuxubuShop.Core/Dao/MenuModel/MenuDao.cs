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
		public List<Menu> ListByGroupId(int groupId)
		{
			return db.Menus.Where(x => x.TypeID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
		}

		// Hiển thị danh sách
		public IEnumerable<Menu> ListAllPaging(string searchString, int page, int pageSize)
		{
			IQueryable<Menu> model = db.Menus;
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.Text.Contains(searchString));
			}
			return model.OrderByDescending(x => x.Text).ToPagedList(page, pageSize);
		}

		// Insert Method
		public long Insert(Menu menu)
		{
			db.Menus.Add(menu);
			db.SaveChanges();
			return menu.ID;
		}

		// Update Method
		public Menu ViewDetail(int id)
		{
			return db.Menus.Find(id);
		}
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
				menu.TypeID = entity.TypeID;
				menu.Status = entity.Status;

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
