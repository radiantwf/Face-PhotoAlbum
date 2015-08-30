using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Face_PhotoAlbum.Views {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TopWindow : UserControl {
        public TopWindow() {
            InitializeComponent();
        }
        private Window GetRootWindow() {
            FrameworkElement parent = this;
            for (parent = (FrameworkElement)parent.Parent; (parent != null && !(parent is Window)); parent = (FrameworkElement)parent.Parent) {

            }
            return ((Window)parent);
        }
        private void TopWindowViewModel_HasWaited(object sender, EventArgs e) {
            Window root = GetRootWindow();
            if (root != null) {
                root.DialogResult = true;
                root?.Close();
            }
            else {
                Application.Current.Shutdown();
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            Window root = GetRootWindow();
            root?.DragMove();
        }
    }
}
