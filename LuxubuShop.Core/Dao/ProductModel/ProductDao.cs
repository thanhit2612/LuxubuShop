using LuxubuShop.Core.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao
{
	public class ProductDao
	{
		LuxubuShopDbContext db = null;
		public ProductDao()
		{
			db = new LuxubuShopDbContext();
		}
		// Danh sách sản phẩm
		public List<Product> ListAll()
		{
			return db.Products.Where(x => x.Status == true).ToList();
		}
		// Danh sach san pham theo danh muc
		public List<Product> ListByCategoryId(long categoryId)
		{
			return db.Products.Where(x => x.CategoryID == categoryId).ToList();
		}
		// Danh sách sản phẩm mới nhât
		public List<Product> ListNewProduct(int top)
		{
			return db.Products.OrderBy(x => x.CreatedDate).Take(top).ToList();
		}
		// Danh sách sản phẩm nổi bật
		public List<Product> ListFeature(int top)
		{
			return db.Products.Where(x => x.Status == true && x.TopHot == true).OrderBy(x => x.TopHot).Take(top).ToList();
		}
		// Get Method
		public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
		{
			IQueryable<Product> model = db.Products;
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.Name.Contains(searchString));
			}
			return model.OrderByDescending(x => x.ClickCount).ToPagedList(page, pageSize);
		}
		// Insert Method
		public long Insert(Product entity)
		{
			entity.CreatedDate = DateTime.Now;
			entity.ExpirationDate = DateTime.Now.AddDays(30);
			db.Products.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}
		public Product GetProductCategoryByID(long id)
		{
			return db.Products.Find(id);
		}
		// Update Method
		public bool Update(Product entity)
		{
			try
			{
				var product = db.Products.Find(entity.ID);
				product.Name = entity.Name;
				product.ProductLink = entity.ProductLink;
				product.MetaDescriptions = entity.MetaDescriptions;
				product.Image = entity.Image;
				product.Price = entity.Price;
				product.CategoryID = entity.CategoryID;
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public Product ViewDetail(long id)
		{
			return db.Products.Find(id);
		}
		// Delete Method
		public bool Delete(int id)
		{
			try
			{
				var product = db.Products.Find(id);
				db.Products.Remove(product);
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
