using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LoginDemo.ViewModels
{
	public interface ILoginViewModel
	{
		string LoginID { get; set; }
		string Password { get; set; }
		ICommand Login { get; }
	}
}
