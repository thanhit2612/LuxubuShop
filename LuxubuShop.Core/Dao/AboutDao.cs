using Common;
using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao
{
	public class AboutDao
	{
		LuxubuShopDbContext db = null;
		public AboutDao()
		{
			db = new LuxubuShopDbContext();
		}
		// View Deatil
		public About ViewDetail(long id)
		{
			return db.Abouts.Find(id);
		}

		// Update Method
		public bool Update(About entity)
		{
			try
			{
				// Xử lý Edit
				var about = db.Abouts.Find(entity.ID);
				// Xử lý Alias
				if (string.IsNullOrEmpty(entity.MetaTitle))
				{
					about.MetaTitle = StringHelper.ToUnsignString(entity.Name);
				}
				if (string.IsNullOrEmpty(entity.MetaDescriptions))
				{
					about.MetaDescriptions = StringHelper.ToUnsignString(entity.Description);
				}
				about.Name = entity.Name;
				about.Description = entity.Description;
				about.Detail = entity.Detail;
				about.Image = entity.Image;
				about.MetaKeywords = entity.MetaKeywords;
				about.Status = entity.Status;
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
