using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using LoginDemo.Models;

namespace LoginDemo.ViewModels
{
	public class LoginViewModel : ObservableObject, ILoginViewModel
	{
		private LoginModel loginModel = new LoginModel();
		private ICommand _LoginCommand;

		public event EventHandler LoginSuccess;
		public event TipsEventHandler LoginFailed;

		protected virtual void OnLoginSuccess()
		{
			var handler = this.LoginSuccess;
			if (handler != null) handler(this, EventArgs.Empty);
		}

		protected virtual void OnLoginFailed(string tips)
		{
			var handler = this.LoginFailed;
			if (handler != null) handler(this, new TipsEventArgs(tips));
		}

		public string LoginID
		{
			get { return this.loginModel.LoginID; }
			set
			{
				this.loginModel.LoginID = value;
				RaisePropertyChanged(() => LoginID);
			}
		}

		public string Password
		{
			get { return this.loginModel.Password; }
			set
			{
				this.loginModel.Password = value;
				RaisePropertyChanged(() => Password);
			}
		}

		public ICommand Login
		{
			get
			{
				if (this._LoginCommand == null)
				{
					this._LoginCommand = new CommandProxy(LoginAction);
				}
				return this._LoginCommand;
			}
		}

		private void LoginAction(object parameter)
		{
			if (this.LoginID != "admin" ||
				this.Password != "123")
			{
				OnLoginFailed("用户名或密码错误！\n正确的用户名是admin\n正确的密码是123");
				return;
			}
			OnLoginSuccess();
		}
	}
}
