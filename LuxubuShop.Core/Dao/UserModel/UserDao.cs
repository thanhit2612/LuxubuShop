﻿using LuxubuShop.Core.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao
{
	public class UserDao
	{
		public static string USER_SESSION = "USER_SESSION";
		LuxubuShopDbContext db = null;
		public UserDao()
		{
			db = new LuxubuShopDbContext();
		}
		// Login Method
		public User GetById(string userName)
		{
			return db.Users.SingleOrDefault(x => x.UserName == userName);
		}
		public bool Login(string userName, string password)
		{
			var result = db.Users.Count(x => x.UserName == userName && x.Password == password);
			if (result > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		// Hiển thị danh sách
		public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
		{
			IQueryable<User> model = db.Users;
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
			}
			return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
		}

		// Insert Method
		public long Insert(User entity)
		{
			entity.CreatedDate = DateTime.Now;

			db.Users.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}
		// Client User Insert Method 
		public long InsertForFacebook(User entity)
		{
			var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
			if (user == null)
			{
				db.Users.Add(entity);
				db.SaveChanges();
				return entity.ID;
			}
			else
			{
			return user.ID;
			}
		}
		// Update Method
		public bool Update(User entity)
		{
			try
			{
				var user = db.Users.Find(entity.ID);
				user.Name = entity.Name;
				user.Email = entity.Email;
				if (!string.IsNullOrEmpty(entity.Password))
				{
					user.Password = entity.Password;
				}
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public User ViewDetail(int id)
		{
			return db.Users.Find(id);
		}

		// Delete Method
		public bool Delete(int id)
		{
			try
			{
				var user = db.Users.Find(id);
				db.Users.Remove(user);
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
