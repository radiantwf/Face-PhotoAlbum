using Face_PhotoAlbum.Views.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Face_PhotoAlbum
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TopWindow : Window
    {
        public TopWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 如果不添加这行代码，则退出按钮是不可用的，因为内置的Close命令是没有实现的，要自己实现
            this.CommandBindings.Add(new CloseCommandBindingProxy(this));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void TopWindowViewModel_HasWaited(object sender, EventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
