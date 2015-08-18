using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginDemo.Views
{
	/// <summary>
	/// Login.xaml 的交互逻辑
	/// </summary>
	public partial class Login : Window
	{
		public Login()
		{
			InitializeComponent();

			// 如果不添加这行代码，则退出按钮是不可用的，因为内置的Close命令是没有实现的，要自己实现
			this.CommandBindings.Add(new CloseCommandBindingProxy(this));
		}

		private void LoginViewModel_LoginSuccess(object sender, EventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			this.Close();
			mainWindow.ShowDialog();
		}

		private void LoginViewModel_LoginFailed(object sender, ViewModels.TipsEventArgs e)
		{
			MessageBox.Show(e.Tips, "提示");
		}
	}
}
