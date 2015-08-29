using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Face_PhotoAlbum.Views.Commands {
    public class ShowDialogCommandBindingProxy : BaseCommandBindingProxy {
        private Window window;
        private CommandBinding _CommandBinding;

        public static ICommand MoveCommand
        {
            private set;
            get;
        }

        public override CommandBinding CommandBinding
        {
            get { return this._CommandBinding; }
        }

        public ShowDialogCommandBindingProxy(Window window)
        {
            this.window = window;
            MoveCommand = new RoutedCommand("ShowDialog", window.GetType());

            _CommandBinding = new CommandBinding(MoveCommand,
                CommandExecuted,
                CommandCanExecute);
        }

        void CommandExecuted(object target, ExecutedRoutedEventArgs e)
        {
            this.window.DragMove();
        }

        void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}