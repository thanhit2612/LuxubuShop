using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuxubuShop.Web.Models
{
	public class LoginModel
	{
		[Key]
		public string UserName { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}