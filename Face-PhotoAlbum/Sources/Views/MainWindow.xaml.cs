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
using System.Windows.Shapes;

namespace Face_PhotoAlbum
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<FaceAlbum_old> m_packagelist;
        private List<Photo_old> m_photolist;
        private List<UserControl> m_packagectllist;
        private List<UserControl> m_photoctllist;
        private FaceAlbum_old m_selectedpackage;
        private Photo_old m_selectedphoto;

        private void InitPackageList()
        {
            m_packagelist = new List<FaceAlbum_old>();
            m_packagelist.Add(new FaceAlbum_old("秋景1", "/Photos/1.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景2", "/Photos/2.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景3", "/Photos/3.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景4", "/Photos/4.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景5", "/Photos/5.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景6", "/Photos/6.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景7", "/Photos/7.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景8", "/Photos/8.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景9", "/Photos/9.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景10", "/Photos/10.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景11", "/Photos/11.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景12", "/Photos/12.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景13", "/Photos/13.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景14", "/Photos/14.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景15", "/Photos/15.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景16", "/Photos/16.jpg"));
            m_packagelist.Add(new FaceAlbum_old("秋景17", "/Photos/17.jpg"));
        }
        //初始化套系控件列表
        private void InitPackageCtlList()
        {
            InitPackageList();
            m_packagectllist = new List<UserControl>();
            foreach (FaceAlbum_old temp in m_packagelist)
            {
                UserControl tempctl = new UserControl();
                tempctl.Margin = new Thickness(10, 10, 10, 10);
                tempctl.Content = temp;
                tempctl.ContentTemplate = (System.Windows.DataTemplate)FindResource("PackageTemplate");
                tempctl.MouseLeftButtonUp += new MouseButtonEventHandler(packagectl_MouseLeftButtonUp);
                tempctl.MouseDoubleClick += new MouseButtonEventHandler(packagectl_MouseDoubleClick);
                m_packagectllist.Add(tempctl);
            }
        }


        //初始化照片列表
        private void InitPhotoList()
        {
            m_photolist = new List<Photo_old>();
            m_photolist.Add(new Photo_old("/Photos/1.jpg"));
            m_photolist.Add(new Photo_old("/Photos/2.jpg"));
            m_photolist.Add(new Photo_old("/Photos/3.jpg"));
            m_photolist.Add(new Photo_old("/Photos/4.jpg"));
            m_photolist.Add(new Photo_old("/Photos/5.jpg"));
            m_photolist.Add(new Photo_old("/Photos/6.jpg"));
            m_photolist.Add(new Photo_old("/Photos/7.jpg"));
            m_photolist.Add(new Photo_old("/Photos/8.jpg"));
            m_photolist.Add(new Photo_old("/Photos/9.jpg"));
            m_photolist.Add(new Photo_old("/Photos/10.jpg"));
            m_photolist.Add(new Photo_old("/Photos/11.jpg"));
            m_photolist.Add(new Photo_old("/Photos/12.jpg"));
            m_photolist.Add(new Photo_old("/Photos/13.jpg"));
            m_photolist.Add(new Photo_old("/Photos/14.jpg"));
            m_photolist.Add(new Photo_old("/Photos/15.jpg"));
            m_photolist.Add(new Photo_old("/Photos/16.jpg"));
        }
        //初始化照片控件列表
        private void InitPhotoCtlList()
        {
            InitPhotoList();

            m_photoctllist = new List<UserControl>();
            foreach (Photo_old temp in m_photolist)
            {
                UserControl tempctl = new UserControl();
                tempctl.Margin = new Thickness(15, 15, 15, 15);
                tempctl.Content = temp;
                tempctl.ContentTemplate = (System.Windows.DataTemplate)FindResource("PhotoTemplate");
                tempctl.MouseLeftButtonUp += new MouseButtonEventHandler(photoctl_MouseLeftButtonUp);
                tempctl.MouseDoubleClick += new MouseButtonEventHandler(photoctl_MouseDoubleClick);
                m_photoctllist.Add(tempctl);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 如果不添加这行代码，则退出按钮是不可用的，因为内置的Close命令是没有实现的，要自己实现
            this.CommandBindings.Add(new CloseCommandBindingProxy(this));

            InitPackageCtlList();
            InitPhotoCtlList();
            ShowPackages();
            m_packagelist[0].IsSelected = true;
            m_selectedpackage = m_packagelist[0];
            m_photolist[0].IsSelected = true;
            m_selectedphoto = m_photolist[0];
        }

        //显示套系列表
        private void ShowPackages()
        {
            this.btn_intopackage.Visibility = System.Windows.Visibility.Visible;
            this.btn_intoscene.Visibility = System.Windows.Visibility.Hidden;
            this.btn_addpackage.Visibility = System.Windows.Visibility.Visible;
            this.btn_delpackage.Visibility = System.Windows.Visibility.Visible;
            this.btn_back1.IsEnabled = false;
            this.border_changjing.Visibility = System.Windows.Visibility.Hidden;
            this.tb_taoxi.Text = string.Format("套系列表({0}套)", this.m_packagelist.Count);

            this.ShowPanel.Children.Clear();
            foreach (UserControl temp in m_packagectllist)
            {
                this.ShowPanel.Children.Add(temp);
            }
        }

        //显示照片列表
        private void ShowPhotos()
        {
            this.btn_intopackage.Visibility = System.Windows.Visibility.Hidden;
            this.btn_intoscene.Visibility = System.Windows.Visibility.Visible;
            this.btn_addpackage.Visibility = System.Windows.Visibility.Hidden;
            this.btn_delpackage.Visibility = System.Windows.Visibility.Hidden;
            this.btn_back1.IsEnabled = true;
            this.border_changjing.Visibility = System.Windows.Visibility.Visible;
            this.tb_changjing.Text = m_selectedpackage.PackageName;

            this.ShowPanel.Children.Clear();
            foreach (UserControl temp in m_photoctllist)
            {
                this.ShowPanel.Children.Add(temp);
            }
        }

        //单击了单个套系
        void packagectl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FaceAlbum_old selectedpackage = (FaceAlbum_old)((UserControl)sender).Content;
            m_selectedpackage.IsSelected = false;
            selectedpackage.IsSelected = true;
            m_selectedpackage = selectedpackage;
        }

        //单击了单个照片
        void photoctl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Photo_old selectedphoto = (Photo_old)((UserControl)sender).Content;
            m_selectedphoto.IsSelected = false;
            selectedphoto.IsSelected = true;
            m_selectedphoto = selectedphoto;
        }

        //双击了套系
        void packagectl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowPhotos();
        }

        //双击了照片
        void photoctl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ShootWindow win = new ShootWindow();
            //win.ShowDialog();
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
            ShowPackages();
        }
        private void btn_intopackage_Click(object sender, RoutedEventArgs e)
        {
            ShowPhotos();
        }
        private void btn_intoscene_Click(object sender, RoutedEventArgs e)
        {
            //ShootWindow win = new ShootWindow();
            //win.ShowDialog();
        }
        
    }
}
