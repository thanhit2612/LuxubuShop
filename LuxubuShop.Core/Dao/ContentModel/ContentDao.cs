using LuxubuShop.Core.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao
{
	public class ContentDao
	{
		LuxubuShopDbContext db = null;
		public ContentDao()
		{
			db = new LuxubuShopDbContext();
		}
		// Danh sách bài viết
		public List<Content> ListAll()
		{
			return db.Contents.Where(x => x.Status == true).ToList();
		}
		// Danh sach san pham theo danh muc
		public List<Content> ListByProductId(long productId)
		{
			return db.Contents.Where(x => x.ProductID == productId).ToList();
		}
		// Get Method
		public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
		{
			IQueryable<Content> model = db.Contents;
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.Name.Contains(searchString));
			}
			return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
		}

		// Insert Method
		public long Insert(Content entity)
		{
			entity.CreatedDate = DateTime.Now;
			db.Contents.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}
		// Update Method
		public bool Update(Content entity)
		{
			try
			{
				var content = db.Contents.Find(entity.ID);
				content.Name = entity.Name;
				content.Descriptions = entity.Descriptions;
				content.Detail = entity.Detail;
				content.ProductLink = entity.ProductLink;
				content.ProductID = entity.ProductID;
				content.Tags = entity.Tags;
				content.Image = entity.Image;
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public Content ViewDetail(long id)
		{
			return db.Contents.Find(id);
		}
		public Content GetContentByID(long id)
		{
			return db.Contents.Find(id);
		}
		// Delete Method
		public bool Delete(int id)
		{
			try
			{
				var content = db.Contents.Find(id);
				db.Contents.Remove(content);
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
