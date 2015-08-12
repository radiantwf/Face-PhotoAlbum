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
using System.Windows.Shapes;

namespace Face_PhotoAlbum
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
        private void btn_quitsys_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btn_back1_Click(object sender, RoutedEventArgs e)
        {
            //ShowPackages();
        }
        private void btn_intopackage_Click(object sender, RoutedEventArgs e)
        {
            //ShowScences();
        }
        private void btn_intoscene_Click(object sender, RoutedEventArgs e)
        {
            //ShootWindow win = new ShootWindow();
            //win.ShowDialog();
        }

        private void btn_set_Click(object sender, RoutedEventArgs e)
        {
            this.menu.IsOpen = true;
        }

    }
}
