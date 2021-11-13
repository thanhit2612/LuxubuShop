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
/*		public static string USER_SESSION = "USER_SESSION";*/
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
				var about = db.Abouts.Find(entity.ID);
				about.Name = entity.Name;
				about.Description = entity.Description;
				about.Detail = entity.Detail;
				about.Image = entity.Image;
				about.CreatedDate = entity.CreatedDate;
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
