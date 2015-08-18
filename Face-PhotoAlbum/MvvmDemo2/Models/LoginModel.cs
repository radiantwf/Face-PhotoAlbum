using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginDemo.Models
{
	public class LoginModel
	{
		public string LoginID { get; set; }
		public string Password { get; set; }

		public LoginModel() { }
		public LoginModel(string loginID, string pwd)
		{
			this.LoginID = loginID;
			this.Password = pwd;
		}
	}
}
