using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao
{
	public class SlideDao
	{
		LuxubuShopDbContext db = null;
		public SlideDao()
		{
			db = new LuxubuShopDbContext();
		}
		// Danh sách slide
		public List<Slide> ListAll()
		{
			return db.Slides.Where(x => x.Status == true).ToList();
		}

		// Insert Method
		public long Insert(Slide entity)
		{
			entity.CreatedDate = DateTime.Now;

			db.Slides.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}

		// Update Method
		public Slide ViewDetail(int id)
		{
			return db.Slides.Find(id);
		}
		public bool Update(Slide entity)
		{
			try
			{
				var slide = db.Slides.Find(entity.ID);
				slide.Image = entity.Image;
				slide.Status = entity.Status;

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
				var slide = db.Slides.Find(id);
				db.Slides.Remove(slide);
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
