using LuxubuShop.Core.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao.ProductModel
{
	public class BrandDao
	{
		LuxubuShopDbContext db = null;
		public BrandDao()
		{
			db = new LuxubuShopDbContext();
		}
		/*<!--START: ADMIN-->*/
		//ListName
		public List<Brand> ListAll()
		{
			return db.Brands.Where(x => x.Status == true).ToList();
		}
		// Insert 
		public long Insert(Brand entity)
		{
			entity.Status = true;
			entity.CreatedDate = DateTime.Now;
			db.Brands.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}
		// Update Method
		public bool Update(Brand entity)
		{
			try
			{
				var brand = db.Brands.Find(entity.ID);
				brand.Name = entity.Name;
				brand.Image = entity.Image;
				brand.Link = entity.Link;
				brand.Status = true;
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public Brand GetByID(long id)
		{
			return db.Brands.Find(id);
		}
		// Delete Method
		public bool Delete(int id)
		{
			try
			{
				var brand = db.Brands.Find(id);
				db.Brands.Remove(brand);
				db.SaveChanges();
				return true;

			}
			catch (Exception)
			{
				return false;
			}
		}
		/*<!--START: ADMIN-->*/
	}
}
