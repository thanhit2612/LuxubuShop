﻿using LuxubuShop.Core.EF;
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
			db.Contents.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}
		public Content GetContentByID(long id)
		{
			return db.Contents.Find(id);
		}
		// Update Method
		public bool Update(Content entity)
		{
			try
			{
				var content = db.Contents.Find(entity.ID);
				content.Name = entity.Name;
				content.Description = entity.Description;
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
		public Content ViewDetail(int id)
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
