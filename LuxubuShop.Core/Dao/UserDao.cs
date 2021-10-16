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
		OnlineShopDbContext db = null;
		public UserDao()
		{
			db = new OnlineShopDbContext();
		}
		// Insert Method
		public long Insert(User entity)
		{
			db.Users.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}
		// Update Method
		public bool Update(User entity)
		{
			try
			{
				var user = db.Users.Find(entity.ID);
				user.Name = entity.Name;
				if (!string.IsNullOrEmpty(entity.Password))
				{
					user.Password = entity.Password;
				}
				user.Email = entity.Email;
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		// Get Method
		public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
		{
			IQueryable<User> model = db.Users;
			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
			}
			return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
		}
		// GetById Method
		public User GetById(string userName)
		{
			return db.Users.SingleOrDefault(x => x.UserName == userName);
		}
		// ViewDetail
		public User ViewDetail(int id)
		{
			return db.Users.Find(id);
		}
		// Login Method
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
