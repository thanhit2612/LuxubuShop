using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuxubuShop.Web.Models
{
	public class RegisterModel
	{
		[Key]
		public long ID { get; set; }

		[Display(Name = "Tài khoản")]
		public string UserName { get; set; }

		[Display(Name = "Mật khẩu")]
		[StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhát 6 ký tự")]
		public string Password { get; set; }

		[Display(Name = "Xác nhận mật khẩu")]
		[Compare("Password", ErrorMessage = "Mật khẩu nhập lại không khớp !")]
		public string ConfirmPassword { get; set; }

		[Display(Name = "Họ và tên")]
		public string Name { get; set; }

		public string Email { get; set; }
	}
}