using LuxubuShop.Core.EF;
using PagedList;
using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuxubuShop.Core.ViewModels;

namespace LuxubuShop.Core.Dao
{
	public class ContentDao
	{
		LuxubuShopDbContext db = null;
		public ContentDao()
		{
			db = new LuxubuShopDbContext();
		}
		/*<!--START: ADMIN-->*/
		// Index
		public List<ContentViewModel> AdminListAll(string searchString, long catID)
		{
			var model = from a in db.Contents
						join b in db.Products
						on a.ProductID equals b.ID

						join c in db.ProductCategories
						on b.CategoryID equals c.ID

						select new ContentViewModel()
						{
							ID = a.ID,
							Name = a.Name,
							Description = a.Description,
							Image = a.Image,
							ProductID = a.ProductID,
							CategoryID = b.CategoryID,
							CateName = c.Name,
							Detail = a.Detail,
							Keywords = a.Keywords,
							CreatedDate = a.CreatedDate,
							CreatedBy = a.CreatedBy,
							MetaTitle = a.MetaTitle,
							MetaDescriptions = a.MetaDescription,
							Status = a.Status,
							ViewCount = a.ViewCount,
							TopHot = a.TopHot,
							Tags = a.Tags,
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
		public long Insert(Content entity)
		{
			// Xử lý Alias
			if (string.IsNullOrEmpty(entity.MetaTitle))
			{
				entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
			}
			if (string.IsNullOrEmpty(entity.MetaDescription))
			{
				entity.MetaDescription = StringHelper.ToUnsignString(entity.Name);
			}
			//Xử lý thêm bài viết
			entity.CreatedDate = DateTime.Now;
			entity.Status = true;
			db.Contents.Add(entity);
			db.SaveChanges();
			// Xử lý Tags
			if (!string.IsNullOrEmpty(entity.Tags))
			{

				string[] tags = entity.Tags.Split(',');
				foreach (var tag in tags)
				{
					var tagId = StringHelper.ToUnsignString(tag);
					var existedTag = this.CheckTag(tagId);
					if (!existedTag)
					{
						this.InsertTag(tagId, tag);
					}
					// Insert To Content Tag
					this.InsertContentTag(entity.ID, tagId);

				}

			}
			return entity.ID;
		}
		//Update
		public Content GetContentByID(long id)
		{
			return db.Contents.Find(id);
		}
		public bool Update(Content entity)
		{
			try
			{
				var content = db.Contents.Find(entity.ID);
				if (string.IsNullOrEmpty(entity.MetaTitle))
				{
					content.MetaTitle = StringHelper.ToUnsignString(entity.Name);
				}
				if (string.IsNullOrEmpty(entity.MetaDescription))
				{
					content.MetaDescription = StringHelper.ToUnsignString(entity.Description);
				}
				content.Name = entity.Name;
				content.Description = entity.Description;
				content.Detail = entity.Detail;
				content.Image = entity.Image;
				content.ProductID = entity.ProductID;
				content.CategoryID = entity.CategoryID;
				content.Tags = entity.Tags;
				content.Keywords = entity.Keywords;
				content.Status = true;
				db.SaveChanges();

				if (!string.IsNullOrEmpty(entity.Tags))
				{
					this.RemoveAllContentTag(entity.ID);
					string[] tags = entity.Tags.Split(',');
					foreach (var tag in tags)
					{
						var tagId = StringHelper.ToUnsignString(tag);
						var existedTag = this.CheckTag(tagId);
						if (!existedTag)
						{
							this.InsertTag(tagId, tag);
						}
						// Insert To Content Tag
						this.InsertContentTag(entity.ID, tagId);

					}

				}
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
		public bool ChangeTopHot(long id)
		{
			var content = db.Contents.Find(id);
			content.TopHot = !content.TopHot;
			db.SaveChanges();
			return content.TopHot;
		}
		public int ClickCount(long id)
		{
			var content = db.Contents.Find(id);
			content.ViewCount += 1;
			db.SaveChanges();
			return content.ViewCount;
		}
		/*<!--END: ADMIN-->*/


		/*<!--START: CLIENT-->*/
		// Index
		public List<Content> ListAll()
		{
			return db.Contents.Where(x => x.Status == true).ToList();
		}
		// List Content By Product
		public List<Content> ListByProductId(long productId)
		{
			var product = db.Products.Find(productId);
			product.ViewCount += 1;
			db.SaveChanges();
			return db.Contents.Where(x => x.ProductID == productId).ToList();
		}
		// Content Details
		public Content ViewDetail(long id)
		{
			return db.Contents.Find(id);
		}
		// Danh sách bài viết liên quan
		public List<Content> ListRelatedContent(long id, int top)
		{
			var content = db.Contents.Find(id);
			return db.Contents.Where(x => x.ID != id && x.CategoryID == content.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
		}
		// Danh sách bài viết nổi bật
		public List<Content> ListFeature(int top)
		{
			return db.Contents.Where((x => x.Status == true && x.TopHot == true)).OrderByDescending(x => x.ViewCount).Take(top).ToList();
		}
		// List Content By Tags
		public IEnumerable<Content> ListAllByTag(string tag, int page, int pageSize)
		{
			var model = (from a in db.Contents
						 join b in db.ContentTags
						 on a.ID equals b.ContentID
						 where b.TagID == tag
						 select new
						 {
							 ID = a.ID,
							 Name = a.Name,
							 Descriptions = a.Description,
							 Image = a.Image,
							 MetaTitle = a.MetaTitle,
							 CreatedDate = a.CreatedDate,
							 CreatedBy = a.CreatedBy,
						 }).AsEnumerable().Select(x => new Content()
						 {
							 ID = x.ID,
							 Name = x.Name,
							 Description = x.Descriptions,
							 Image = x.Image,
							 MetaTitle = x.MetaTitle,
							 CreatedDate = x.CreatedDate,
							 CreatedBy = x.CreatedBy,
						 });
			return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
		}
		/*<!--END: CLIENT-->*/


		/*<!--START: TAGS-->*/
		public List<Tag> ListTag(long contentId)
		{
			var model = (from a in db.Tags
						 join b in db.ContentTags
						 on a.ID equals b.TagID
						 where b.ContentID == contentId
						 select new
						 {
							 ID = b.TagID,
							 Name = a.Name
						 }).AsEnumerable().Select(x => new Tag()
						 {
							 ID = x.ID,
							 Name = x.Name,
						 }
						);
			return model.ToList();
		}
		public Tag GetTag(string id)
		{
			return db.Tags.Find(id);
		}
		public void InsertTag(string id, string name)
		{
			var tag = new Tag();
			tag.ID = id;
			tag.Name = name;
			db.Tags.Add(tag);
			db.SaveChanges();
		}
		public void InsertContentTag(long contentId, string tagId)
		{
			var contentTag = new ContentTag();
			contentTag.ContentID = contentId;
			contentTag.TagID = tagId;
			db.ContentTags.Add(contentTag);
			db.SaveChanges();
		}
		public bool CheckTag(string id)
		{
			return db.Tags.Count(x => x.ID == id) > 0;
		}
		public void RemoveAllContentTag(long contentId)
		{
			db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentId));
			db.SaveChanges();
		}
		/*<!--END: TAGS-->*/
		public ContentViewModel Detail(long id)
		{
			var model = from a in db.Contents
						join b in db.Products
						on a.ProductID equals b.ID

						join c in db.Brands
						on b.BrandID equals c.ID

						where a.ID == id
						select new ContentViewModel
						{
							ID = a.ID,
							Name = a.Name,
							MetaTitle = a.MetaTitle,
							Description = a.Description,
							MetaDescriptions = a.MetaDescription,
							Detail = a.Detail,
							ProductLink = b.ProductLink,
							BrandID = c.ID,
							BrandName = c.Name,
						};
			return model.SingleOrDefault();
		}
	}
}
