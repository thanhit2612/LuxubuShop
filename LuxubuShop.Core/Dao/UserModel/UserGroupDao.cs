using LuxubuShop.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxubuShop.Core.Dao.UserModel
{
	public class UserGroupDao
	{
		LuxubuShopDbContext db = null;
		public UserGroupDao()
		{
			db = new LuxubuShopDbContext();
		}
		public List<UserGroup> ListAll()
		{
			return db.UserGroups.Where(x => x.Status == true).ToList();
		}
	}
}
