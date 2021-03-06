﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Face_PhotoAlbum.Views.Commands {
    public class MinimizedCommandBindingProxy : BaseCommandBindingProxy {
        private Window window;
        private CommandBinding _CommandBinding;

        public static ICommand MinimizedCommand
        {
            private set;
            get;
        }

        public override CommandBinding CommandBinding
        {
            get { return this._CommandBinding; }
        }

        public MinimizedCommandBindingProxy(Window window)
        {
            this.window = window;
            MinimizedCommand = new RoutedCommand("Minimized", window.GetType());

            _CommandBinding = new CommandBinding(MinimizedCommand,
                CommandExecuted,
                CommandCanExecute);
        }

        void CommandExecuted(object target, ExecutedRoutedEventArgs e)
        {
            this.window.WindowState = WindowState.Minimized;
        }

        void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}