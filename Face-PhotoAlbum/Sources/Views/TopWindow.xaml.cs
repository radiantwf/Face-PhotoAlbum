using System;
using System.Windows;
using System.Windows.Input;

namespace Face_PhotoAlbum.Views {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TopWindow : Window {
        public TopWindow() {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void TopWindowViewModel_HasWaited(object sender, EventArgs e) {
            this.DialogResult = true;
            this.Close();
        }
    }
}
