using Common;
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
		// Danh sách tên sản phẩm
		public List<string> ListName(string keyword)
		{
			return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
		}
		// Danh sach san pham theo danh muc
		public List<Product> ListByCategoryId(long categoryId)
		{
			return db.Products.Where(x => x.CategoryID == categoryId).ToList();
		}
		// Danh sách sản phẩm tìm kiếm được.
		public IEnumerable<Product> Search(string keyword, int page, int pageSize)
		{
			IQueryable<Product> model = db.Products;
			if (!string.IsNullOrEmpty(keyword))
			{
				model = model.Where(x => x.Name.Contains(keyword));
			}
			return model.OrderByDescending(x => x.Name).ToPagedList(page, pageSize);
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
			// Xử lý Alias
			if (string.IsNullOrEmpty(entity.MetaTitle))
			{
				entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
			}
			if (string.IsNullOrEmpty(entity.MetaDescriptions))
			{
				entity.MetaDescriptions = StringHelper.ToUnsignString(entity.Name);
			}

			entity.CreatedDate = DateTime.Now;
			entity.ExpirationDate = DateTime.Now.AddDays(30);
			entity.ClickCount = 0;
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
				if (string.IsNullOrEmpty(entity.MetaTitle))
				{
					product.MetaTitle = StringHelper.ToUnsignString(entity.Name);
				}
				if (string.IsNullOrEmpty(entity.MetaDescriptions))
				{
					product.MetaDescriptions = StringHelper.ToUnsignString(entity.Descriptions);
				}
				product.CategoryID = entity.CategoryID;
				product.Name = entity.Name;
				product.Descriptions = entity.Descriptions;
				product.Image = entity.Image;
				product.Price = entity.Price;
				product.PromotionPrice = entity.PromotionPrice;
				product.ProductLink = entity.ProductLink;
				product.MetaKeywords = entity.MetaKeywords;
				product.Status = entity.Status;
				product.TopHot = entity.TopHot;
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
		public bool ClickCount(long id)
		{
			var product = db.Products.Find(id);
			product.ClickCount += 1;
			db.SaveChanges();
			return true;
		}

	}
}
