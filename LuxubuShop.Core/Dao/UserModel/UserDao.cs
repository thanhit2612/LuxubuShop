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
	public class UserDao
	{
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
		public int LoginAdmin(string userName, string passWord, bool isLoginAdmin = false)
		{
			var result = db.Users.SingleOrDefault(x => x.UserName == userName);
			if (result == null)
			{
				return 0;
			}
			else
			{
				if (isLoginAdmin == true)
				{
					if (result.GroupID == CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.MOD_GROUP)
					{
						if (result.Password == passWord)
							return 1;
						else
							return -2;
					}
					else
					{
						return -3;
					}
				}
				else
				{
					if (result.Password == passWord)
						return 1;
					else
						return -2;
				}
			}
		}
		public int LoginUser(string userName, string passWord, bool isLoginAdmin = false)
		{
			var result = db.Users.SingleOrDefault(x => x.UserName == userName);
			if (result == null)
			{
				return 0;
			}
			else
			{
				if (isLoginAdmin == true)
				{
					if (result.GroupID == CommonConstants.MEMBER_GROUP || (result.GroupID == CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.MOD_GROUP))
					{
						if (result.Password == passWord)
							return 1;
						else
							return -2;
					}
					else
					{
						return -3;
					}
				}
				else
				{
					if (result.Password == passWord)
						return 1;
					else
						return -2;
				}
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
				user.Status = true;
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
		public User GetById(int id)
		{
			return db.Users.Find(id);
		}
		public bool Update(User entity)
		{
			try
			{
				var user = db.Users.Find(entity.ID);
				user.Name = entity.Name;
				user.Email = entity.Email;
				user.Status = true;
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

		public bool CheckUserName(string userName)
		{
			return db.Users.Count(x => x.UserName == userName) > 0;
		}
		public bool CheckEmail(string email)
		{
			return db.Users.Count(x => x.Email == email) > 0;
		}
		public bool ForgotPassWord(User entity)
		{
			try
			{
				var user = db.Users.Where(x => x.UserName == entity.UserName).SingleOrDefault();
				user.UserName = entity.UserName;
				user.Password = entity.Password;
				user.Status = true;
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
