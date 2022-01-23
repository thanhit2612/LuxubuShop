using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Common;

namespace LuxubuShop.Core.Dao
{
	public class ProductCategoryDao
	{
		LuxubuShopDbContext db = null;
		public ProductCategoryDao()
		{
			db = new LuxubuShopDbContext();
		}
		/*<!--START: ADMIN-->*/
		// Index
		public IEnumerable<ProductCategory> AdminListAll(string searchString, int page, int pageSize)
		{
			IQueryable<ProductCategory> model = db.ProductCategories;
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.Name.Contains(searchString));
			}
			return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
		}
		//Insert
		public long Insert(ProductCategory entity)
		{
			if (string.IsNullOrEmpty(entity.MetaTitle))
			{
				entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
			}
			entity.CreatedDate = DateTime.Now;
			entity.Status = true;
			db.ProductCategories.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}
		// Update
		public ProductCategory GetById(long id)
		{
			return db.ProductCategories.Find(id);
		}
		public bool Update(ProductCategory entity)
		{
			try
			{
				var proCategory = db.ProductCategories.Find(entity.ID);
				if (string.IsNullOrEmpty(entity.MetaTitle))
				{
					proCategory.MetaTitle = StringHelper.ToUnsignString(entity.Name);
				}
				proCategory.Name = entity.Name;
				proCategory.Image = entity.Image;
				proCategory.Status = true;
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		// Delete
		public bool Delete(int id)
		{
			try
			{
				var proCategory = db.ProductCategories.Find(id);
				db.ProductCategories.Remove(proCategory);
				db.SaveChanges();
				return true;

			}
			catch (Exception)
			{
				return false;
			}
		}
		/*<!--END: ADMIN-->*/

		/*<!--START: CLIENT-->*/
		// List All
		public List<ProductCategory> ListAll()
		{
			return db.ProductCategories.Where(x => x.Status == true).ToList();
		}
		/*<!--END: CLIENT-->*/
	}
}
