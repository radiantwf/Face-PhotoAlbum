﻿using Face_PhotoAlbum.Views.Commands;
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

namespace Face_PhotoAlbum.Views {
    /// <summary>
    /// AlbumWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AlbumWindow : Window {
        public AlbumWindow()
        {
            InitializeComponent();

            this.CommandBindings.Add(new CloseCommandBindingProxy(this));
            this.CommandBindings.Add(new MinimizedCommandBindingProxy(this));
        }
    }
}
