using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
namespace LuxubuShop.Core.Dao
{
	public class ProductCategoryDao
	{
		LuxubuShopDbContext db = null;
		public ProductCategoryDao()
		{
			db = new LuxubuShopDbContext();
		}
		public List<ProductCategory> ListAll()
		{
			return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
		}
		// Get Method
		public IEnumerable<ProductCategory> ListAllPaging(string searchString, int page, int pageSize)
		{
			IQueryable<ProductCategory> model = db.ProductCategories;
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.Name.Contains(searchString));
			}
			return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
		}

		// Insert Method
		public long Insert(ProductCategory entity)
		{
			entity.CreatedDate = DateTime.Now;
			db.ProductCategories.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}
		public ProductCategory ParentID(long id)
		{
			return db.ProductCategories.Find(id);
		}
		// Update Method
		public bool Update(ProductCategory entity)
		{
			try
			{
				var proCategory = db.ProductCategories.Find(entity.ID);
				proCategory.Name = entity.Name;
				proCategory.ParentID = entity.ParentID;
				proCategory.ShowOnHome = entity.ShowOnHome;
				proCategory.SeoTitle = entity.SeoTitle;
				proCategory.MetaDescriptions = entity.MetaDescriptions;
				proCategory.Status = entity.Status;
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public ProductCategory ViewDetail(int id)
		{
			return db.ProductCategories.Find(id);
		}
		// Delete Method
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
	}
}
