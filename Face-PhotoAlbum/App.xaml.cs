using Face_PhotoAlbum.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Face_PhotoAlbum {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            CommandingBootstrapper bootstrapper = new CommandingBootstrapper();
            bootstrapper.Run();
        }
    }
}
