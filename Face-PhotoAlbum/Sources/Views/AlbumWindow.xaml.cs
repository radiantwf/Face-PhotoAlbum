using System;
using System.Windows;
using System.Windows.Input;
using Face_PhotoAlbum.Views.Commands;

namespace Face_PhotoAlbum.Views {
    /// <summary>
    /// AlbumWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AlbumWindow : Window {
        public AlbumWindow()
        {
            this.CommandBindings.Add(new CloseCommandBindingProxy(this));
            this.CommandBindings.Add(new MinimizedCommandBindingProxy(this));
            this.CommandBindings.Add(new MoveCommandBindingProxy(this));

            InitializeComponent();
        }

        private void AlbumContainerViewModel_OnShowPhotoDetailWindow(object sender, EventArgs e) {
            //MainWindow mainWindow = new MainWindow();
            //this.Close();
            //mainWindow.ShowDialog();
        }
    }
}
