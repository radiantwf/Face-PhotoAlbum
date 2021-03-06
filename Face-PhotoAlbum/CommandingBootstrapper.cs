﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System.Windows;
using Face_PhotoAlbum.Views;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Face_PhotoAlbum {
    public class CommandingBootstrapper : UnityBootstrapper {
        protected override DependencyObject CreateShell() {
            return Container.Resolve<AlbumWindow>();
        }

        protected override void InitializeShell() {
            base.InitializeShell();
            TopWindow top = new TopWindow();
            top.ShowDialog();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog() {
            base.ConfigureModuleCatalog();
        }

    }
}
