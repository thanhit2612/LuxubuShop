using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao
{
	public class BannerDao
	{
		LuxubuShopDbContext db = null;
		public BannerDao()
		{
			db = new LuxubuShopDbContext();
		}
		// Danh sách Banner
		public List<Banner> ListAll()
		{
			return db.Banners.Where(x => x.Status == true).ToList();
		}

		// Insert Method
		public long Insert(Banner entity)
		{
			entity.CreatedDate = DateTime.Now;
			entity.Status = true;

			db.Banners.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}

		// Update Method
		public Banner GetByID(int id)
		{
			return db.Banners.Find(id);
		}
		public bool Update(Banner entity)
		{
			try
			{
				var banner = db.Banners.Find(entity.ID);
				banner.Image = entity.Image;
				banner.Link = entity.Link;
				entity.Status = true;

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
				var banner = db.Banners.Find(id);
				db.Banners.Remove(banner);
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
