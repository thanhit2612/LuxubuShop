using Common;
using LuxubuShop.Core.EF;
using LuxubuShop.Core.ViewModels;
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
		/*<!--START: ADMIN-->*/
		//ListName
		public List<Product> ListAll()
		{
			return db.Products.Where(x => x.Status == true).ToList();
		}
		// Index
		public List<ProductViewModel> ListAllPaging(string searchString,long catID)
		{
			var model = from a in db.Products
						join b in db.ProductCategories
						on a.CategoryID equals b.ID
						select new ProductViewModel()
						{

							ID = a.ID,
							Name = a.Name,
							CategoryID = a.CategoryID,
							CateName = b.Name,
							Description = a.Description,
							Image = a.Image,
							Price = a.Price,
							PromotionPrice = a.PromotionPrice,
							ProductLink = a.ProductLink,
							CreatedDate = a.CreatedDate,
							ExpirationDate = a.ExpirationDate,
							CreatedBy = a.CreatedBy,
							MetaTitle = a.MetaTitle,
							MetaDescription = a.MetaDescription,
							Keywords = a.Keywords,
							Status = a.Status,
							ViewCount = a.ViewCount,
							TopHot = a.TopHot,
							Rating = a.Rating,
						};
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.Name.Contains(searchString)).OrderByDescending(x => x.ViewCount);
			}
			if (catID >= 1)
			{
				return model.Where(x => x.CategoryID == catID).OrderByDescending(x => x.ViewCount).ToList();
			}
			return model.OrderByDescending(x => x.ViewCount).ToList();
		}

		// Insert
		public long Insert(Product entity)
		{
			if (string.IsNullOrEmpty(entity.MetaTitle))
			{
				entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
			}
			if (string.IsNullOrEmpty(entity.MetaDescription))
			{
				entity.MetaDescription = StringHelper.ToUnsignString(entity.Name);
			}

			entity.CreatedDate = DateTime.Now;
			entity.ExpirationDate = DateTime.Now.AddDays(30);
			entity.ViewCount = 0;
			entity.Status = true;
			db.Products.Add(entity);
			db.SaveChanges();
			return entity.ID;
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
				if (string.IsNullOrEmpty(entity.MetaDescription))
				{
					product.MetaDescription = StringHelper.ToUnsignString(entity.Description);
				}
				product.CategoryID = entity.CategoryID;
				product.Name = entity.Name;
				product.Description = entity.Description;
				product.Image = entity.Image;
				product.Price = entity.Price;
				product.PromotionPrice = entity.PromotionPrice;
				product.ProductLink = entity.ProductLink;
				product.Keywords = entity.Keywords;
				entity.Status = true;
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
				var content = db.Contents.Select(x => x.ProductID).SingleOrDefault();
				if (content > 0)
				{
					
					db.Contents.Remove(db.Contents.Find(content));
				}
				db.Products.Remove(product);
				db.SaveChanges();
				return true;

			}
			catch (Exception)
			{
				return false;
			}
		}
		public bool ChangeTopHot(long id)
		{
			var product = db.Products.Find(id);
			product.TopHot = !product.TopHot;
			db.SaveChanges();
			return product.TopHot;
		}
		/*<!--START: ADMIN-->*/

		/*<!--START: CLIENT-->*/
		/*<!--END: CLIENT-->*/
		// Danh sách sản phẩm
		public Product GetProductCategoryByID(long id)
		{
			return db.Products.Find(id);
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
			return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
		}
		// Danh sách sản phẩm nổi bật
		public List<Product> ListFeature(int top)
		{
			return db.Products.Where(x => x.Status == true && x.TopHot == true).OrderByDescending(x => x.ViewCount).Take(top).ToList();
		}
		// Get Method
		
		public int ClickCount(long id)
		{
			var product = db.Products.Find(id);
			product.ViewCount += 1;
			db.SaveChanges();
			return product.ViewCount;
		}

	}
}
