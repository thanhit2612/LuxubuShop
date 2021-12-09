using LuxubuShop.Core.EF;
using PagedList;
using System;
using System.Collections.Generic;
using Common;
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
		// Danh sách bài viết theo Tags
		public IEnumerable<Content> ListAllByTag(string tag, int page, int pageSize)
		{
			var model = (from a in db.Contents
										join b in db.ContentTags
										on a.ID equals b.ContentID
										where b.TagID == tag
										select new {
											ID = a.ID,
											Name = a.Name,
											Descriptions = a.Descriptions,
											Image = a.Image,
											MetaTitle = a.MetaTitle,
											CreatedDate = a.CreatedDate,
											CreatedBy = a.CreatedBy,
										}).AsEnumerable().Select(x => new Content() {
											ID = x.ID,
											Name = x.Name,
											Descriptions = x.Descriptions,
											Image = x.Image,
											MetaTitle = x.MetaTitle,
											CreatedDate = x.CreatedDate,
											CreatedBy = x.CreatedBy,
										});
			return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
		}

		// Insert Method
		public long Insert(Content entity)
		{
			// Xử lý Alias
			if (string.IsNullOrEmpty(entity.MetaTitle))
			{
				entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
			}
			//Xử lý thêm bài viết
			entity.CreatedDate = DateTime.Now;
			db.Contents.Add(entity);
			db.SaveChanges();
			// Xử lý Tags
			if (!string.IsNullOrEmpty(entity.Tags))
			{

				string[] tags = entity.Tags.Split(',');
				foreach(var tag in tags)
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
		// Update Method
		public bool Update(Content entity)
		{
			try
			{
				// Xử lý Alias
				if (string.IsNullOrEmpty(entity.MetaTitle))
				{
					entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
				}

				// Xử lý Edit
				var content = db.Contents.Find(entity.ID);
				content.Name = entity.Name;
				content.Descriptions = entity.Descriptions;
				content.Detail = entity.Detail;
				content.ProductLink = entity.ProductLink;
				content.ProductID = entity.ProductID;
				content.Tags = entity.Tags;
				content.Image = entity.Image;
				db.SaveChanges();

				// Xử lý Tags
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


		/*TAG*/
		// Handle Tags
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
		

	}
}
